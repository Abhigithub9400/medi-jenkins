<template>
    <div class="container">
      <header class="header-container">
        <div class="logo" @click="redirectToHome">
          <img src="@/assets/medical-icon.png" alt="Medical Icon" class="icon" />
          <h3>MEDIASSIST</h3>
        </div>
      </header>
  
      <main class="content">
        <section class="guidelines">
          <h2>Guidelines</h2>
          <p>
            Welcome to MEDIASSIST! Please follow these guidelines to ensure a smooth experience:
          </p>
          <ul>
            <li>Ensure that all personal information is accurate and up-to-date.</li>
            <li>Adhere to the terms and conditions outlined in our policy.</li>
            <li>Respect the privacy of other users and their information.</li>
            <li>Report any issues or concerns to our support team promptly.</li>
          </ul>
        </section>
      </main>
    </div>
  </template>
  
  <script setup>
  import { useRouter } from 'vue-router';
  
  const router = useRouter();
  
  const redirectToHome = () => {
    router.push('/'); // Redirect to the home page
  };
  </script>
  
  <style scoped>
  @import url('https://fonts.googleapis.com/css2?family=Karma:wght@400;500&display=swap');
  
  .container {
    display: flex;
    flex-direction: column;
    height: 100vh;
  }
  
  .header-container {
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: #f8f9fa;
    padding: 20px;
  }
  
  .logo {
    display: flex;
    align-items: center;
    cursor: pointer;
  }
  
  .logo h3 {
    font-family: 'Karma', sans-serif;
    font-size: 26px;
    color: #0066D4;
    margin: 0;
  }
  
  .icon {
    width: 35px;
    height: 40px;
    margin-right: 10px;
  }
  
  .content {
    flex: 1;
    padding: 20px;
    max-width: 800px;
    margin: 0 auto;
  }
  
  .guidelines {
    font-family: 'Karma', sans-serif;
  }
  
  .guidelines h2 {
    font-size: 24px;
    color: #333;
  }
  
  .guidelines ul {
    list-style: disc;
    margin-left: 20px;
  }
  </style>
  