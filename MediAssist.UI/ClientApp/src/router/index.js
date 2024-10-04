import { createRouter, createWebHistory } from 'vue-router';
import LandingPage from '../components/LandingPage.vue';
import LoginPage from '../components/LoginPage.vue';
import SignUpPage from '../components/SignUpPage.vue';
import HomePage from '../components/HomePage.vue';
import ConversationPage from '../components/ConversationPage.vue';
import ScanImagePage from '../components/ScanImagePage.vue';
import GuidelinesPage from '../components/GuidelinesPage.vue';
import PrivacyPolicyPage from '../components/PrivacyPolicyPage.vue';

const routes = [
  { path: '/', component: LandingPage },
  { path: '/login', component: LoginPage },
  { path: '/SignUp', component: SignUpPage },
  { path: '/home', component: HomePage },
  { path: '/conversation', component: ConversationPage },
  { path: '/scanImage', component: ScanImagePage },
  { path: '/terms-and-conditions', component: GuidelinesPage },
  { path: '/privacy-policy', component: PrivacyPolicyPage } // Updated path for consistency
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
