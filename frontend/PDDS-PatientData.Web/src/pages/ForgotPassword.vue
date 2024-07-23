<template>
    <div class="card border-0 shadow rounded-3 my-5">
        <div class="card-body p-4 p-sm-5">
            <h5 class="card-title text-center mb-3 fw-bold fs-5">Forgot Password</h5>
            <div class="text-start mb-3 small">
                <label>Enter the email address associated with your account. If found, we'll send you a link to reset your password.</label>
            </div>
            <form @submit="doResetPassword">
                <div class="form-floating mb-3">
                    <input type="email" class="form-control" v-model="emailAddress" @keyup.enter="doResetPassword()" 
                        placeholder="name@example.com" required>
                    <label for="floatingInput">Email Address</label>
                </div>
                <div class="d-grid">
                    <button class="btn btn-primary btn-login text-uppercase fw-bold">Send Password Reset Link</button>
                </div>
                <div class="text-start mt-3" v-show="showInfoMsg" :class="infoMsgColor">
                    <label id="infoMsg">{{ infoMsg }}</label>
                </div>
                <div class="mt-3 float-end small">
                    <a href="/sign-in">Return to Sign In</a>
                </div>
            </form>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from 'vuex';

export default defineComponent({
  data() {
    return {
      emailAddress: '',
      showInfoMsg: false,
      infoMsg: 'Default msg',
      infoMsgColor: 'success'
    }
  },
  methods: {
    ...mapActions("Auth", ["resetPassword"]),
    async doResetPassword(event: Event) {
      event.preventDefault();

      try {
        var data = { emailAddress: this.emailAddress };
        await this.resetPassword(data);

        this.infoMsg = "Reset password link will be delivered shortly."
        this.infoMsgColor = 'success';
        this.showInfoMsg = true;

        setTimeout(() => {
          this.showInfoMsg = false;
        }, 3000);

      } catch (error) {
        console.error(error);

        this.infoMsg = "Oops! Something went wrong..."
        this.infoMsgColor = 'error';
        this.showInfoMsg = true;
      }
    }
  }
});
</script>

<style scoped>
    .success { color: green}
    .error { color: #ff0000; }
</style>