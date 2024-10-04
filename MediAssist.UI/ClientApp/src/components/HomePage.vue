<template>
  <div class="home-page">
    <div class="background-section">
      <div class="content-area">
        <h1 class="welcome-heading">Welcome to MediAssist!</h1>
        <p class="welcome-text">
          MediAssist AI Monitoring, a cutting-edge web application that transforms healthcare monitoring. 
          Our AI-driven platform offers real-time insights, predictive analytics, and detailed reporting, 
          ensuring superior patient care and efficient medical processes.
        </p>
        <div class="button-group">
          <router-link to="/conversation" class="start-chat-button">Start Video Chat</router-link>
          <router-link to="/scanImage" class="upload-scan-button">Upload and Scan</router-link>
        </div>
      </div>
    </div>

    <div class="key-features">
      <div class="feature left-feature">
        <div >🩺 <b>Enhanced Patient Care</b></div>
        <p>
          By providing timely and accurate health data, the app empowers healthcare providers to deliver 
          superior patient care.
        </p>
        <div >📊 <b>Predictive Analytics</b></div>
        <p>
          Our AI-driven platform offers predictive analytics, enabling proactive healthcare management and 
          personalized patient care.
        </p>
        <div >🩺 <b>Enhanced Patient Care</b></div>
        <p>
          By providing timely and accurate health data, the app empowers healthcare providers to deliver 
          superior patient care.
        </p>
      </div>
      <div class="feature right-feature">
        <div >📊 <b>Predictive Analytics</b></div>
        <p>
          Our AI-driven platform offers predictive analytics, enabling proactive healthcare management and 
          personalized patient care.
        </p>
        <div >🩺 <b>Enhanced Patient Care</b></div>
        <p>
          By providing timely and accurate health data, the app empowers healthcare providers to deliver 
          superior patient care.
        </p>
        <div >📊 <b>Predictive Analytics</b></div>
        <p>
          Our AI-driven platform offers predictive analytics, enabling proactive healthcare management and 
          personalized patient care.
        </p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HomePage'
}
</script>

<style scoped>
.home-page {
  font-family: 'Arial', sans-serif;
  color: #333;
}

.background-section {
  position: relative;
  width: 100%;
  height: 464px;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}

.background-section::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-image: url('@/assets/home-background.jpg');
  background-size: cover;
  background-position: center;
  z-index: 1;
}

.background-section::after {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: linear-gradient(221.47deg, rgba(0, 13, 32, 0) -126.54%, rgba(0, 9, 22, 0.82) 76.54%);
  z-index: 2;
}

.content-area {
  position: relative;
  width: 80%; 
  max-width: 1301px; 
  color: white;
  padding: 50px 10px; 
  box-sizing: border-box;
  z-index: 3;
}

.welcome-heading {
  font-size: 2.5em;
  margin-bottom: 10px;
}

.welcome-text {
  font-size: 1.2em;
  margin-bottom: 20px;
}

.button-group {
  display: flex;
  gap: 10px;
  flex-wrap: wrap; 
}

.start-chat-button,
.upload-scan-button {
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  color: white;
  background-color: #0066D4;
  cursor: pointer;
  font-size: 1em;
  text-decoration: none;
}

.upload-scan-button {
  background-color: #FFFFFF;
  color: black;
}

.start-chat-button:hover {
  background-color: #005bb5;
}

.upload-scan-button:hover {
  background-color: #dcdfe1;
}

.key-features {
  display: flex;
  justify-content: space-between;
  padding: 20px;
  box-sizing: border-box;
  
  p{
    padding-left: 30px;
  }
}

.feature {
  width: 45%;
}

.icon {
  font-size: 2em;
  margin-bottom: 10px;
}

h2 {
  font-size: 1.5em;
  margin-bottom: 10px;
}

p {
  font-size: 1em;
}
</style>


