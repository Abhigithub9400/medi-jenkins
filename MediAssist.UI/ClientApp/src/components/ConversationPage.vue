<template>
  <div class="conversation-page">
    <div class="sidebar">
      <ul>
        <li>
          <router-link to="/">
            <i class="pi pi-home" style="color: white"></i>
          </router-link>
        </li>
        <!-- <li><i class="pi pi-wrench" style="color: white"></i></li>
        <li><i class="pi pi-heart" style="color: white"></i></li> -->
      </ul>
    </div>

    <div class="main-content">
      <div class="conversations">
        <div class="conversation-header">
          <h2>Your conversations</h2>
          <div class="conversation-actions">
            <button class="start-session" @click="toggleSession">{{ recording ? 'Stop session' : 'Start session' }}</button>
            <button class="generate-results" @click="generateResults">Generate Results</button>
          </div>
        </div>
        <div class="conversation-content">
          <div class="conversation-audio">
            <img v-if="recording" src="@/assets/waveform-animated.gif" alt="Recording Waveform" class="centered-image" />
            <img v-else src="@/assets/waveform-static.png" alt="Static Waveform" class="centered-image" />
          </div>
          <div class="conversation-text">
            <ul>
              <li v-for="(message, index) in conversation" :key="index" :class="{'doctor-message': message.sender === 'doctor', 'patient-message': message.sender === 'patient'}">
                <span class="icon-wrapper">
                  <img v-if="message.sender === 'doctor'" src="@/assets/doctor-icon.png" alt="Doctor Icon" class="icon" />
                  <img v-else src="@/assets/patient-icon.png" alt="Patient Icon" class="icon" />
                </span>
                <span class="message-text">{{ message.text }}</span>
              </li>
            </ul>
          </div>
        </div>
      </div>

      <div class="diagnosis-questions">
        <div class="potential-diagnosis">
          <h3>Potential Diagnosis</h3>
          <ul>
            <li v-for="(diagnosis, index) in diagnosisList" :key="index">
              <strong>{{ diagnosis.title }}</strong><br>{{ diagnosis.description }}
            </li>
          </ul>
        </div>

        <div class="suggested-questions">
          <h3>Suggested Questions</h3>
          <ul>
            <li v-for="(question, index) in suggestedQuestions" :key="index">{{ question }}</li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { io } from "socket.io-client";
import 'primeicons/primeicons.css';

export default {
  data() {
    return {
      conversation: [
        { sender: 'doctor', text: 'Have you been in contact with anyone who’s been sick recently, or have you traveled anywhere in the past few weeks?' },
        { sender: 'patient', text: 'No, I haven’t traveled, and I don’t think I’ve been around anyone who’s sick.' },
        { sender: 'doctor', text: 'Okay, let’s start by checking your vital signs...' },
      ],
      diagnosisList: [
        { title: 'Common Cold or Flu', description: 'Recent exposure to sick individuals...' },
        { title: 'COVID-19', description: 'Recent exposure, travel history, vaccination status.' },
        { title: 'Sinusitis', description: 'History of allergies...' },
        { title: 'Meningitis', description: 'Rapid onset, presence of neurological symptoms...' }
      ],
      suggestedQuestions: [
        'Have you noticed any other symptoms, such as nausea, dizziness, or changes in vision?',
        'Are you experiencing any difficulty breathing, chest pain, or palpitations?',
      ],
      recording: false,
      audioChunks: [],
      mediaRecorder: null,
      socket: null,
    };
  },
  methods: {
    async toggleSession() {
      if (this.recording) {
        this.stopRecording();
        this.disconnectSocket();
      } else {
        this.connectSocket();
        this.startRecording();
      }
      this.recording = !this.recording;
    },
    connectSocket() {
      this.socket = io('YOUR_BACKEND_URL');
      
      this.socket.on('transcription', (message) => {
        this.conversation.push(message);
      });

      this.socket.on('diagnosis-updated', (data) => {
        this.diagnosisList = data.diagnosis;
        this.suggestedQuestions = data.questions;
      });
    },
    disconnectSocket() {
      if (this.socket) {
        this.socket.disconnect();
        this.socket = null;
      }
    },
    startRecording() {
      navigator.mediaDevices.getUserMedia({ audio: true })
        .then(stream => {
          this.mediaRecorder = new MediaRecorder(stream);
          this.mediaRecorder.ondataavailable = event => {
            this.audioChunks.push(event.data);
            if (this.socket && this.socket.connected) {
              const audioBlob = new Blob([event.data], { type: 'audio/wav' });
              this.socket.emit('audio-data', audioBlob);
            }
          };
          this.mediaRecorder.onstop = () => {
            const audioBlob = new Blob(this.audioChunks, { type: 'audio/wav' });
            this.audioChunks = [];
            this.sendAudioToBackend(audioBlob);
          };
          this.mediaRecorder.start();
        })
        .catch(error => console.error('Error accessing audio stream:', error));
    },
    stopRecording() {
      if (this.mediaRecorder && this.mediaRecorder.state !== 'inactive') {
        this.mediaRecorder.stop();
        this.mediaRecorder.stream.getTracks().forEach(track => track.stop());
      }
    },
    sendAudioToBackend(audioBlob) {
      const formData = new FormData();
      formData.append('audio', audioBlob, 'audio.wav');
      
      axios.post('YOUR_BACKEND_URL/transcribe', formData)
        .then(response => {
          const messages = response.data.messages;
          messages.forEach(message => {
            this.conversation.push(message);
          });
        })
        .catch(error => console.error('Error sending audio to backend:', error));
    },
    async generateResults() {
      const response = await axios.get('YOUR_BACKEND_URL/generate-results');
      this.diagnosisList = response.data.diagnosis;
      this.suggestedQuestions = response.data.questions;
    }
  },
  beforeUnmount() {
    this.disconnectSocket();
    if (this.mediaRecorder && this.mediaRecorder.state !== 'inactive') {
      this.mediaRecorder.stream.getTracks().forEach(track => track.stop());
    }
  }
};
</script>

<style scoped>
.conversation-page {
  display: flex;
  background-color: #f5f7fa;
}

.sidebar {
  width: 80px;
  background-color: #00376b;
  padding: 20px 0;
  text-align: center;
}

.sidebar ul {
  list-style: none;
  padding: 0;
}

.sidebar ul li {
  margin: 20px 0;
}

.main-content {
  flex: 1;
  padding: 20px;
}

.conversations {
  background: white;
  border-radius: 10px;
  padding: 20px;
  margin-bottom: 20px;
}

.conversation-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.conversation-header h2 {
  margin: 0;
}

.conversation-actions {
  display: flex;
  gap: 10px;
}

.start-session {
  background-color: #007bff;
  border: none;
  color: white;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
}

.generate-results {
  background-color: white;
  border: 1px solid #0066D4;
  color: #0066D4;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
}

.start-session:hover {
  background-color: #005bb5;
}

.generate-results:hover {
  background-color: #edf6ff;
}

.conversation-content {
  display: flex;
  align-items: flex-start;
  gap: 20px;
}

.conversation-audio {
  flex: 1;
  width: 428px;
  height: 272px;
  background: linear-gradient(180deg, rgba(244, 249, 255, 0.62) 0%, #C2DFFF 50%, rgba(205, 229, 255, 0.31) 100%);
  position: relative;
}

.centered-image {
  width: 100%;
  height: auto;
  display: block;
  margin: auto;
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
}

.conversation-text {
  flex: 2;
  width: 428px;
  height: 272px;
  background: #FFFFFF;
  border-radius: 10px;
  padding: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  opacity: 1;
  overflow-y: auto;
  margin-left: 4px;
  box-sizing: border-box;
}

.conversation-text ul {
  list-style: none;
  padding: 0;
}

.conversation-text ul li {
  display: flex;
  align-items: flex-start;
  margin-bottom: 10px;
}

.icon-wrapper {
  width: 30px;
  height: 30px;
  margin-right: 10px;
}

.icon {
  width: 100%;
  height: 100%;
  border-radius: 50%;
  object-fit: cover;
}

.message-text {
  flex: 1;
}

.diagnosis-questions {
  display: flex;
  justify-content: space-between;
  gap: 20px;
}

.potential-diagnosis, .suggested-questions {
  width: 320px;  /* Fixed width */
  height: 272px; /* Fixed height */
  background: white;
  border-radius: 10px;
  padding: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  overflow-y: auto;
}

.potential-diagnosis {
  width: 48%; /* Adjusted width */
}

.suggested-questions {
  width: 48%; /* Adjusted width */
}

.potential-diagnosis h3, .suggested-questions h3 {
  margin-bottom: 15px;
}

.potential-diagnosis ul, .suggested-questions ul {
  list-style: none;
  padding: 0;
}

.potential-diagnosis ul li, .suggested-questions ul li {
  margin-bottom: 10px;
}
</style>
