<template>
    <div class="card border-0 shadow rounded-3 my-5">
        <div class="card-body p-4 p-sm-5">
            <h5 class="card-title text-center mb-3 fw-bold fs-5">Reset Password</h5>
            <form @submit="doSavePassword">
                <div class="form-floating mb-3">
                    <input type="password" class="form-control" v-model="password" required>
                    <label for="password">New Password</label>
                </div>
                <div class="form-floating mb-3">
                    <input type="password" class="form-control" v-model="confirmPassword" @keyup.enter="doSavePassword()" required>
                    <label for="confirmPassword">Confirm Password</label>
                </div>
                <div class="d-grid">
                    <button class="btn btn-primary btn-login text-uppercase fw-bold">Save Password</button>
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
      password: '',
      confirmPassword: '',
      showInfoMsg: false,
      infoMsg: 'Default msg',
      infoMsgColor: 'success'
    }
  },
  methods: {
    ...mapActions("Auth", ["savePassword"]),
    async doSavePassword(event: Event) {
      event.preventDefault();

      try {
        var data = { emailAddress: this.emailAddress };
        await this.savePassword(data);

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