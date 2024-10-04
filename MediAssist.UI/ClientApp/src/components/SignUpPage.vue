<template>
    <div class="signup-container">
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
                    Already have an account?
                    <a href="#" @click="redirectToLogin">Sign In</a>
                </p>
            </header>

            <div class="form-content">
                <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
                <h2 class="form-heading">Sign Up</h2>

                <form @submit.prevent="handleSignUp">
                    <div class="input-group">
                        <label for="name">Name</label>
                        <input v-model="name"
                               type="text"
                               id="name"
                               @focus="interactWithField('name')"
                               @blur="validateField('name')"
                               placeholder="Enter your name"
                               required />
                        <span v-if="errors.name" class="error">{{ errors.name }}</span>
                    </div>

                    <div class="input-group">
                        <label for="email">Email</label>
                        <input v-model="email"
                               type="email"
                               id="email"
                               @focus="interactWithField('email')"
                               @blur="validateField('email')"
                               placeholder="Enter your email"
                               required />
                        <span v-if="errors.email" class="error">{{ errors.email }}</span>
                    </div>

                    <div class="input-group password-group">
                        <label for="password">
                            Password
                            <span class="info-icon" @mouseover="showTooltip" @mouseleave="hideTooltip">
                                <i class="pi pi-info-circle"></i>
                            </span>
                        </label>
                        <div class="password-wrapper">
                            <input v-model="password"
                                   :type="passwordVisible ? 'text' : 'password'"
                                   id="password"
                                   @focus="interactWithField('password')"
                                   @blur="validateField('password')"
                                   placeholder="Enter your password"
                                   required />
                            <button type="button" @click="togglePasswordVisibility">
                                <span v-if="passwordVisible"><i class="pi pi-eye-slash"></i></span>
                                <span v-else><i class="pi pi-eye"></i></span>
                            </button>
                            <!-- Tooltip now positioned above the info icon -->
                            <div class="message-tooltip" v-if="tooltipVisible">
                                <div class="message-content">
                                    <p>Password must include:</p>
                                    <ul>
                                        <li>At least 8 characters</li>
                                        <li>One uppercase letter</li>
                                        <li>One lowercase letter</li>
                                        <li>One digit</li>
                                        <li>One special character</li>
                                    </ul>
                                </div>
                                <div class="message-arrow"></div>
                            </div>
                        </div>
                        <span v-if="errors.password" class="error">{{ errors.password }}</span>
                    </div>

                    <div class="input-group">
                        <label for="confirmPassword">Confirm Password</label>
                        <div class="password-wrapper">
                            <input v-model="confirmPassword"
                                   :type="passwordVisible ? 'text' : 'password'"
                                   id="confirmPassword"
                                   @focus="interactWithField('confirmPassword')"
                                   @blur="validateField('confirmPassword')"
                                   placeholder="Confirm your password"
                                   required />
                        </div>
                        <span v-if="errors.confirmPassword" class="error">{{ errors.confirmPassword }}</span>
                    </div>

                    <div class="terms-privacy">
                        <input type="checkbox" id="agreeTerms" v-model="agreeTerms"
                               @focus="interactWithField('agreeTerms')"
                               @blur="validateField('agreeTerms')"
                               required />
                        <span v-if="errors.agreeTerms" class="error">{{ errors.agreeTerms }}</span>
                        <label for="agreeTerms">
                            I agree to the
                            <a href="/terms-and-conditions" target="_blank">Terms and Conditions</a> and
                            <a href="/privacy-policy" target="_blank">Privacy Policy</a>.
                        </label>
                    </div>

                    <button type="submit" class="signup-button" :disabled="!agreeTerms">Create Account</button>
                </form>
            </div>
        </div>
    </div>
</template>



<script setup>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';

    const router = useRouter();
    const name = ref('');
    const errorMessage = ref('');
    const email = ref('');
    const password = ref('');
    const confirmPassword = ref('');
    const passwordVisible = ref(false);
    const tooltipVisible = ref(false);
    const agreeTerms = ref(false);
    const errors = ref({
        name: '',
        email: '',
        password: '',
        confirmPassword: '',
        agreeTerms: ''
    });
    const interacted = ref({
        name: false,
        email: false,
        password: false,
        confirmPassword: false,
        agreeTerms: false
    });

    const togglePasswordVisibility = () => {
        passwordVisible.value = !passwordVisible.value;
    };

    const showTooltip = () => {
        tooltipVisible.value = true;
    };

    const hideTooltip = () => {
        tooltipVisible.value = false;
    };

    const interactWithField = (field) => {
        interacted.value[field] = true;
    };

    const validateField = (field) => {
        if (!interacted.value[field]) {
            return;
        }
        switch (field) {
            case 'name': {
                const namePattern = /^[A-Za-z\s.]+$/;
                if (!name.value) {
                    errors.value.name = 'Name is required.';
                } else if (!namePattern.test(name.value)) {
                    errors.value.name = 'Name must contain only alphabetic characters.';
                } else if (name.value.length < 3) {
                    errors.value.name = 'Name must be at least 3 characters long.';
                } else {
                    errors.value.name = '';
                }
                break;
            }
            case 'email': {
                const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (!email.value) {
                    errors.value.email = 'Email is required.';
                } else if (!emailPattern.test(email.value)) {
                    errors.value.email = 'Enter a valid email address.';
                } else {
                    errors.value.email = '';
                }
                break;
            }
            case 'password': {
                if (!password.value) {
                    errors.value.password = 'Password is required.';
                } else if (password.value.length < 6) {
                    errors.value.password = 'Password must be at least 6 characters long.';
                } else {
                    errors.value.password = '';
                }
                break;
            }
            case 'confirmPassword': {
                if (!confirmPassword.value) {
                    errors.value.confirmPassword = 'Please confirm your password.';
                } else if (confirmPassword.value !== password.value) {
                    errors.value.confirmPassword = 'Passwords do not match.';
                } else {
                    errors.value.confirmPassword = '';
                }
                break;
            }
            case 'agreeTerms': {
                if (!agreeTerms.value) {
                    errors.value.agreeTerms = 'Please confirm your password.';
                } else {
                    errors.value.agreeTerms = '';
                }
                break;
            }
        }
    };

    const handleSignUp = async () => {
        // Validate all fields on submit
        validateField('name');
        validateField('email');
        validateField('password');
        validateField('confirmPassword');
        validateField('agreeTerms');

        if (!errors.value.name && !errors.value.email && !errors.value.password && !errors.value.confirmPassword && !errors.value.agreeTerms) {
            try {
                const response = await fetch('/api/manage/signup', {
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
                    router.push({ path: data.redirectUrl, query: { successMessage: 'Account created successfully. Please log in.' } });
                } else {
                    errorMessage.value = data.message;
                }
            } catch (error) {
                console.error('Error during signup:', error);
            }
        }
    };

    const redirectToLogin = () => {
        router.push('/login'); // Redirect to the Login page
    };
</script>

<style scoped>
    @import url('https://fonts.googleapis.com/css2?family=Karma:wght@400;500&display=swap');

    .error {
        color: red;
        font-size: 12px;
    }

    .error-message {
        color: red;
        font-size: 14px;
        margin-bottom: 10px;
    }

    .signup-container {
        display: flex;
        height: 100vh;
        overflow: hidden;
        flex-direction: row;
    }

    .image-side {
        flex: 1;
        display: flex;
        align-items: center;
        justify-content: center;
        overflow: hidden;
    }

        .image-side img {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }

    .form-side {
        flex: 1;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        padding: 20px;
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
            display: flex;
            align-items: center;
            margin-bottom: 5px;
            color: #6D6D6D;
        }

        .input-group input {
            width: 100%;
            padding: 10px;
            box-sizing: border-box;
            border-radius: 4px;
            border: 1px solid #ccc;
            font-family: 'Karma', sans-serif;
            color: #474545;
            font-size: 14px;
        }

    .password-group {
        position: relative;
    }

    .password-wrapper {
        position: relative;
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

    .pi {
        padding: 4px 10px 0px 0px;
    }

    .info-icon {
        margin-left: 10px;
        font-size: 14px;
        color: #6D6D6D;
        cursor: pointer;
    }

    .message-tooltip {
        position: absolute;
        top: -180px;
        left: 60px;
        background: #f7f8fa; /* Grey background */
        color: #333;
        padding: 10px;
        border-radius: 10px;
        max-width: 250px;
        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
        font-size: 12px;
        z-index: 10;
    }

    .message-arrow {
        position: absolute;
        top: 100%;
        left: 20px;
        width: 0;
        height: 0;
        border-left: 10px solid transparent;
        border-right: 10px solid transparent;
        border-top: 10px solid #f7f8fa; /* Match the tooltip background color */
    }

    .signup-button {
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

    .terms-privacy {
        display: flex;
        align-items: center;
        margin-bottom: 20px;
    }

        .terms-privacy input {
            margin-right: 10px;
        }

        .terms-privacy a {
            color: #13bef0;
            cursor: pointer;
            text-decoration: none;
        }

            .terms-privacy a:hover {
                text-decoration: underline;
            }

    @media (max-width: 768px) {
        .signup-container {
            flex-direction: column;
        }

        .header-container {
            flex-direction: column;
            align-items: center;
        }

        .form-content {
            padding: 20px;
            border-radius: 8px;
        }

        .form-heading {
            font-size: 20px;
            text-align: center;
        }

        .logo {
            flex-direction: column;
            align-items: center;
            margin-bottom: 10px;
        }

        .icon {
            width: 30px;
            height: 35px;
            margin-right: 0;
            margin-bottom: 5px;
        }

        .input-group input {
            font-size: 13px;
        }

        .signup-button {
            font-size: 14px;
        }

        .message-tooltip {
            max-width: 200px;
            top: -160px;
        }

        .terms-privacy {
            flex-direction: column;
            align-items: flex-start;
        }

            .terms-privacy label {
                margin-left: 0;
                font-size: 12px;
            }
    }

    @media (max-width: 480px) {
        .image-side img {
            object-fit: cover;
        }

        .form-content {
            padding: 15px;
        }

        .form-heading {
            font-size: 18px;
        }

        .input-group input {
            font-size: 12px;
        }

        .signup-button {
            font-size: 13px;
            padding: 8px;
        }

        .message-tooltip {
            max-width: 180px;
        }

        .terms-privacy {
            font-size: 11px;
        }
    }
</style>


