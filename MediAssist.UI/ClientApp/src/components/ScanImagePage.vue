<template>
    <div class="scan-image-page">
      <div class="sidebar">
        <ul>
          <li>
            <router-link to="/">
              <i class="pi pi-home"></i>
            </router-link>
          </li>
          <!-- <li><i class="pi pi-wrench"></i></li>
          <li><i class="pi pi-heart"></i></li> -->
        </ul>
      </div>
  
      <div class="main-content">
        <div class="header-section">
            <h2>Scan Image and Detect</h2>
          <div class="header-icons">
            <!-- <i class="pi pi-bell"></i> -->
            <img src="@/assets/doctor-icon.png" alt="Doctor Icon" class="doctor-icon" />
          </div>
        </div>
  
        <div class="scan-image-section">
          <div class="scan-header">
            <button class="generate-results-btn" :disabled="!fileUploaded" @click="generateResults">Generate Results</button>
          </div>
  
          <div class="scan-content">
            <div class="upload-box" @click="triggerFileInput">
              <input type="file" ref="fileInput" @change="handleFileUpload" hidden />
              <div v-if="!fileUploaded" class="upload-content">
                <img src="@/assets/upload-icon.png" />
                <p>
                  Drop or Drag image here<br />
                  or <a href="#" class="browse-link">Browse</a> to Upload
                </p>
              </div>
              <div v-if="fileUploaded" class="uploaded-image-container">
                <img :src="uploadedImage" alt="Uploaded Image" class="uploaded-image" />
              </div>
            </div>
            <div v-if="resultText" class="result-box">
              <div class="result-content">
                <p>{{ resultText }}</p>
              </div>
            </div>
            <div v-else class="result-box">
              <div class="result-content">
                <img src="@/assets/no-file.png" />
                <p>No document uploaded yet.<br />Please upload to generate results</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  import 'primeicons/primeicons.css';
  
  export default {
    name: "ScanImagePage",
    data() {
      return {
        fileUploaded: false,
        resultText: '',
        uploadedImage: '',
      };
    },
    methods: {
      triggerFileInput() {
        this.$refs.fileInput.click();
      },
      handleFileUpload(event) {
        const file = event.target.files[0];
        if (file) {
          this.fileUploaded = true;
          this.uploadedImage = URL.createObjectURL(file); 
        }
      },
      async generateResults() {
        if (!this.fileUploaded) return;
  
        const formData = new FormData();
        formData.append('file', this.$refs.fileInput.files[0]);
  
        try {
          const response = await axios.post('/api/scan', formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          });
          this.resultText = response.data.result;
        } catch (error) {
          console.error('Error generating results:', error);
        }
      }
    }
  };
  </script>
  
  <style scoped>
  .scan-image-page {
    display: flex;
    background-color: #f3f9ff;
    height: 100vh;
    box-sizing: border-box;
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
    padding-left: 20px;
    display: flex;
    flex-direction: column;
    height: 100%;
  }
  
  .header-section {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: white;
    border-radius: 10px;
    margin-bottom: 20px;

    h2{
      padding-left: 10px;
    }
  }
  
  .search-bar {
    width: 673px;
    border: 1px solid #dee2e8;
    border-radius: 45px;
    display: flex;
    align-items: center;
    gap: 10px;
    padding: 5px;
  }
  
  .search-bar input {
    padding: 10px;
    width: 100%;
    border: none;
    outline: none;
    border-radius: 45px;
  }
  
  .header-icons {
    display: flex;
    align-items: center;
    gap: 20px;
  }
  
  .header-icons i {
    font-size: 20px;
    color: #007bff;
  }
  
  .doctor-icon {
    padding-right: 10px;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    object-fit: cover;
  }
  
  .scan-image-section {
    flex: 1;
    background-color: white;
    border-radius: 10px;
    padding: 20px;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
  }
  
    .scan-header {
    display: flex;
    justify-content: space-between;
    align-items: baseline;
  }

  .generate-results-btn {
    background-color: #007bff;
    border: none;
    color: white;
    padding: 10px 20px;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin-left: auto; /* Aligns the button to the right */
  }

  .generate-results-btn:disabled {
    background-color: #a0a0a0;
    cursor: not-allowed;
  }

  .generate-results-btn:hover {
    background-color: #0056b3;
  }

  
  .scan-content {
    display: flex;
    justify-content: space-between;
    gap: 20px;
    flex-grow: 1;
  }
  
  .upload-box {
    width: 48%;
    border-radius: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
    box-sizing: border-box;
    padding: 20px;
    height: 100%;
    background-color: #edf6ff;
    border: 2px dashed #7BAFE3;
    cursor: pointer;
    position: relative;
    overflow: hidden;
  }
  
  .upload-box:hover {
    background-color: #d0eaff;
  }
  
  .upload-content {
    text-align: center;
    color: #353535;
  }
  
  .upload-content img {
    width: 84px;
    height: 84px;
    transition: opacity 0.3s ease;
  }
  
  .upload-content p {
    margin: 0;
    font-size: 16px;
  }
  
  .uploaded-image-container {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    overflow: hidden;
  }
  
  .uploaded-image {
    width: 100%;
    height: 100%;
    object-fit: cover; 
    opacity: 0.7;
  }
  
  .result-box {
    width: 48%;
    border-radius: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
    box-sizing: border-box;
    padding: 20px;
    background-color: white;
  }
  
  .result-content {
    text-align: center;
    color: #353535;
  }
  
  .result-content img {
    width: 82.23px;
    height: 95.67px;
  }
  
  .result-content p {
    margin: 0;
    font-size: 16px;
  }
  
  .browse-link {
    font-weight: bold;
    color: #0066d4;
    text-decoration: none;
    cursor: pointer;
  }
  
  .browse-link:hover {
    text-decoration: underline;
  }
  
  .pi {
    color: white;
  }
  </style>
  