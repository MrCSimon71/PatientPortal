<template>
    <div class="card border-0 shadow rounded-3 my-5">
        <div class="card-body p-4 p-sm-5">
            <h5 class="card-title text-center mb-5 fw-bold fs-5">Sign In</h5>
            <div class="text-start mb-3">
              <label>Valid Credentials: admin / master</label>
            </div>
            <div class="text-start mb-3" v-show="showInfoMsg" :class="infoMsgColor">
                <label id="infoMsg">{{ infoMsg }}</label>
            </div>
            <form @submit="doLogin">
                <div class="form-floating mb-3">
                    <input id="username" type="text" class="form-control" v-model="username" placeholder="name@example.com">
                    <label for="username">Username</label>
                </div>
                <div class="form-floating mb-3">
                    <input id="password" type="password" class="form-control" v-model="password" placeholder="Password">
                    <label for="password">Password</label>
                </div>
                <div class="form-check mb-3">
                    <input id="rememberPassword" class="form-check-input" type="checkbox" value="" v-model="rememberPassword">
                    <label class="form-check-label float-start" for="rememberPassword">
                        Remember password
                    </label>
                </div>
                <div class="d-grid">
                    <button class="btn btn-primary btn-login text-uppercase fw-bold">Sign in</button>
                </div>
                <div class="mt-3 float-start">
                    <a href="/forgot-password">Forgot Password?</a>
                </div>
                <!-- <hr class="my-4">
                <div class="d-grid mb-2">
                    <button class="btn btn-google btn-login text-uppercase fw-bold" type="submit">
                        <i class="fab fa-google me-2"></i> Sign in with Google
                    </button>
                </div>
                <div class="d-grid">
                    <button class="btn btn-facebook btn-login text-uppercase fw-bold" type="submit">
                        <i class="fab fa-facebook-f me-2"></i> Sign in with Facebook
                    </button>
                </div> -->
            </form>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from 'vuex';
import { LoginModel } from '@/models/LoginModel';

export default defineComponent({
  data() {
    return {
      username: 'admin',
      password: 'master',
      rememberPassword: '',
      showInfoMsg: false,
      infoMsg: 'Default msg',
      infoMsgColor: 'success'
    }
  },
  methods: {
    ...mapActions("Auth", ["logIn"]),
    async doLogin(event: Event) {
      event.preventDefault();

      try {
        const data = {
          username: this.username,
          password: this.password
        } as LoginModel;

        await this.logIn(data);

        this.$router.push('/patients');

      } catch (error) {
        console.error(error);
        this.infoMsg = "Oops! Something went wrong..."
        this.infoMsgColor = 'error';
        this.showInfoMsg = true;
      }
    },
    onForgotPassword() {
      console.log("Go to forgot passwrod page");
      this.$router.push('/forgot-password');
    }
  }
});
</script>

<style scoped>
  .success { color: green}
  .error { color: #ff0000; }
</style>