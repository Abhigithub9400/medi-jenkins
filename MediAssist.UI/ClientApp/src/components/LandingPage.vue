<template>
  <div class="landing-page">
    <div>
      <header class="header">
        <div class="logo" @click="reloadPage">
          <img src="@/assets/medical-icon.png" alt="Medical Icon" class="icon"/>
          <h3>MEDIASSIST</h3>
        </div>

        <nav class="nav-menu">
          <router-link to="/home" class="nav-link">Home</router-link>
          <router-link to="/about" class="nav-link">About</router-link>
          <router-link to="/features" class="nav-link">Features</router-link>
          <router-link to="/resources" class="nav-link">Resources</router-link>
        </nav>

        <div class="buttons">
          <router-link to="/login" class="btn login-btn">Login</router-link>
          <router-link to="/signup" class="btn signup-btn">Sign Up</router-link>
        </div>
      </header>

      <div class="central-box">
        <h1 class="central-heading">AI-Powered Clinical Decision Support</h1>
        <p class="central-paragraph">
          MediAssist AI Monitoring is your all-in-one solution for advanced healthcare management. Our AI-powered platform delivers real-time insights, predictive analytics, and comprehensive reporting to empower healthcare professionals to provide the best care possible.
        </p>
      </div>

      <div class="video-section">
        <div class="video-box">
          <video controls>
            <source src="@/assets/demo-video.mp4" type="video/mp4"/>
            Your browser does not support the video tag.
          </video>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'LandingPage',
  methods: {
    reloadPage() {
      window.location.reload();
    },
  },
};
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Karma:wght@400;500&display=swap');

.landing-page {
  position: relative;
  background-color: #00152D;
  background-size: cover;
  background-position: center;
  min-height: 100vh;
  color: #fff;
  overflow: auto;
  font-family: Lato, Helvetica, Arial, sans-serif !important;
  padding: 20px; 
}

.header {
  display: flex;
  justify-content: space-between;
  padding: 20px;
  align-items: center;
  flex-wrap: wrap; 
}

.logo {
  display: flex;
  align-items: center;
  cursor: pointer;
  margin-right: 20px;
}

.logo h3 {
  font-family: 'Karma', sans-serif;
  font-size: 26px;
  font-weight: 500;
  color: #0066D4;
  margin: 0; 
}

.icon {
  width: 35px;
  height: 40px;
  margin-right: 10px;
}

.nav-menu {
  display: flex;
  justify-content: center;
  flex-grow: 1;
  flex-wrap: wrap;
  margin-top: 10px; 
}

.nav-link {
  color: #fff;
  margin: 10px 15px; 
  text-decoration: none;
  font-size: 16px;
  font-weight: 500;
  transition: color 0.3s;
}

.nav-link:hover {
  color: #0066cc;
  transform: scale(1.05);
}

.buttons {
  display: flex;
  align-items: center;
  flex-wrap: wrap;
  margin-top: 10px; 
}

.btn {
  background-color: #223a66;
  border: 1px;
  color: #fff;
  padding: 10px 20px;
  margin-left: 10px;
  border-radius: 8px;
  text-decoration: none;
  text-align: center;
  transition: background-color 0.3s, transform 0.3s;
  font-weight: 600;
  font-size: 16px;
  margin-bottom: 10px;
}

.btn:hover {
  background-color: #1a2b4d;
  transform: scale(1.05);
}

.login-btn {
  background-color: #0066cc;
}

.login-btn:hover {
  background-color: #005bb5;
}

.signup-btn {
  background-color: #00152D;
  border: 1px solid #FFFFFF;
}

.signup-btn:hover {
  background-color: #373838;
}

.central-box {
  margin: 40px auto;
  width: 955px;
  height: 168px;
  text-align: center;
}

.central-heading {
  font-size: 50px;
  font-weight: 600;
  margin-bottom: 20px;
}

.central-paragraph {
  font-weight: 400;
  font-size: 18px;
  line-height: 27px;
  width: 915px;
  height: 81px;
}

.video-section {
  display: flex;
  justify-content: center;
  margin: 40px 0;
}

.video-box {
  width: 1039px;
  height: 494px;
  border-radius: 15px;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0,0,0,0.2);
}

video {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

@media (max-width: 912px) {
  .central-box {
    width: 90%; 
    height: auto; 
    padding: 20px; 
  }

  .central-heading {
    font-size: 28px;
  }

  .central-paragraph {
    font-size: 16px;
    width: 100%;
    height: 150px;
  }

  .video-box {
    width: 100%;
    height: auto;
  }

  .nav-menu, .buttons {
    justify-content: center;
    margin: 10px 0;
  }

  .btn {
    margin: 5px 10px; 
    padding: 8px 15px;
  }

  .nav-link {
    margin: 5px 10px; 
  }
}

</style>
