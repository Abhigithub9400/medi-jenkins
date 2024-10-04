<template>
    <div class="login-container">
        <div class="image-side">
            <img src="@/assets/loginPageImage.jpg" alt="Background Image" />
        </div>

        <div class="form-side">
            <header class="header-container">
                <div class="logo" @click="reloadPage">
                    <img src="@/assets/medical-icon.png" alt="Medical Icon" class="icon" />
                    <h3>MEDIASSIST</h3>
                </div>
                <p class="signup-option">
                    Don’t have an account?
                    <a href="#" @click="redirectToSignUp">SignUp</a>
                </p>
            </header>
            <div class="form-content">
                <div v-if="successMessage" class="success-message">{{ successMessage }}</div>
                <h2 class="form-heading">Login</h2>

                <form @submit.prevent="handleLogin">
                    <div class="input-group">
                        <label for="email">Email</label>
                        <input v-model="email" type="text" id="email" required />
                    </div>

                    <div class="input-group">
                        <label for="password">Password</label>
                        <div class="password-wrapper">
                            <input v-model="password"
                                   :type="passwordVisible ? 'text' : 'password'"
                                   id="password"
                                   required />
                            <button type="button" @click="togglePasswordVisibility">
                                <span v-if="passwordVisible"><i class="pi pi-eye-slash"></i></span>
                                <span v-else><i class="pi pi-eye"></i></span>
                            </button>
                        </div>
                    </div>

                    <div class="options">
                        <label>
                            <input type="checkbox" v-model="rememberMe" />
                            Remember me
                        </label>
                        <a href="#" class="forgot-password">Forgot Password?</a>
                    </div>
                    <button type="submit" class="login-button" @click.prevent="handleLogin">Login</button>
                    <div class="google-signup">
                        <p>Or sign up with</p>
                        <button type="button" class="google-button" @click.prevent="handleGoogleSignIn">
                            <img src="@/assets/google.png" alt="Google" /> Google
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { onMounted,onUnmounted } from 'vue';
    import { ref } from 'vue';
    import { useRoute, useRouter } from 'vue-router';

    const route = useRoute();
    const router = useRouter();
    const email = ref('');
    const password = ref('');
    const rememberMe = ref(false);
    const passwordVisible = ref(false);
    const successMessage = ref('');

    onMounted(() => {
        successMessage.value = route.query.successMessage || '';
        document.addEventListener('click', clearSuccessMessage);
    });

    onUnmounted(() => {
        document.removeEventListener('click', clearSuccessMessage);
    });

    const togglePasswordVisibility = () => {
        passwordVisible.value = !passwordVisible.value;
    };

    const clearSuccessMessage = () => {
        successMessage.value = '';
    };

    const handleLogin = async () => {
        try {
            const response = await fetch('/api/manage/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Email: email.value,
                    Password: password.value
                })
            });

            const data = await response.json();

            if (response.ok && data.success) {
                router.push(data.redirectUrl);  
            } else {
                alert(data.message);  
            }
        } catch (error) {
            console.error('Error during login:', error);
        }
    };


    const handleGoogleSignIn = async () => {
        try {
            const response = await fetch('/api/login/google-signin', { method: 'GET' });
            const data = await response.json();

            if (data.redirectUrl) {
                window.location.href = data.redirectUrl;
            } else {
                alert('Error during Google sign-in.');
            }
        } catch (error) {
            console.error('Error during Google sign-in:', error);
        }
    };

    const reloadPage = () => {
        window.location.reload();
    };

    const redirectToSignUp = () => {
        router.push('/signup');
    };
</script>


<style scoped>
    @import url('https://fonts.googleapis.com/css2?family=Karma:wght@400;500&display=swap');

    .login-container {
        display: flex;
        height: 100vh;
        overflow: hidden;
    }

    .success-message {
        color: green;
        font-size: 14px;
        margin-bottom: 10px;
    }

    .image-side {
        flex: 1;
        position: relative;
        display: flex;
        align-items: center;
        justify-content: center;
        overflow: hidden;
    }

        .image-side img {
            width: 100%;
            height: 100%;
        }

    .form-side {
        flex: 1;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        padding: 20px;
        position: relative;
        background-color: white;
    }

    .header-container {
        display: flex;
        justify-content: space-between;
        align-items: center;
        width: 100%;
        max-width: 600px;
        padding: 20px;
        box-sizing: border-box;
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

    .signup-option {
        font-size: 14px;
        color: #333;
    }

        .signup-option a {
            color: #13bef0;
            text-decoration: none;
        }

    .form-content {
        width: 100%;
        max-width: 600px;
        padding: 40px;
        box-sizing: border-box;
        border: 1px solid #ccc;
        border-radius: 10px;
        background-color: white;
    }

    .form-heading {
        margin-top: 0;
        margin-bottom: 20px;
        font-size: 24px;
        font-weight: 600;
        color: #333;
        text-align: left;
    }

    .input-group {
        margin-bottom: 15px;
    }

        .input-group label {
            display: block;
            margin-bottom: 5px;
            color: #6D6D6D;
        }

        .input-group input {
            width: 100%;
            padding: 10px;
            box-sizing: border-box;
            border-radius: 4px;
            border: 1px solid #ccc;
            font-family: var(--sans-serif-font);
            color: #474545;
            font-size: var(--delta-font-size);
        }

    .password-wrapper {
        position: relative;
    }

    .pi {
        padding: 4px 10px 0px 10px;
    }

    .password-wrapper input {
        padding-right: 30px;
        border-radius: 4px;
        border: 1px solid #ccc;
    }

    .password-wrapper button {
        position: absolute;
        right: 0;
        top: 9px;
        background: transparent;
        border: none;
        cursor: pointer;
    }

    .forgot-password {
        color: #0066D4;
    }

    .options {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding-bottom: 20px;
    }

    .login-button {
        font-weight: 600;
        font-size: 16px;
        width: 100%;
        padding: 10px;
        background-color: #13bef0;
        color: white;
        border: none;
        cursor: pointer;
        border-radius: 4px;
    }

    .google-signup {
        text-align: center;
        font-weight: 400;
        font-size: 14px;
    }

    .google-button {
        font-weight: 500;
        font-size: 14px;
        display: flex;
        align-items: center;
        justify-content: center;
        background: #F9F9F9;
        border: 1px solid #ccc;
        padding: 10px;
        cursor: pointer;
        width: 100%;
        border-radius: 4px;
    }

    .login-button:hover {
        background-color: #005bb5;
    }

    .google-button:hover {
        background-color: #dcdfe1;
    }

    .google-button img {
        width: 20px;
        height: 20px;
        margin-right: 10px;
    }

    @media screen and (max-width: 768px) {
        .login-container {
            flex-direction: column;
        }

        .header-container {
            padding: 10px;
        }

        .form-content {
            padding: 20px;
            width: 90%;
            margin: 20px auto;
        }
    }
</style>
