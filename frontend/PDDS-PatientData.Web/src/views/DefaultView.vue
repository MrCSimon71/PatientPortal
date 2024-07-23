<template>
  <router-view></router-view>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from "vuex";
//import moment from "moment";

export default defineComponent({
  data() {
    return {
      timerId: 0,
    };
  },
  mounted() {
    document.body.classList.add('defaultLayout-body', 'defaultLayout-body-pd');
    require('@/assets/scripts/sidebar.ts');
    this.setActiveMenu();
    this.startSessionCheckTimer();
  },
  unmounted() {
    document.body.classList.remove('defaultLayout-body', 'defaultLayout-body-pd');
    clearInterval(this.timerId);
  },
  updated() {
    this.setActiveMenu();
  },
  methods: {
    ...mapActions("Auth", ["logOut", "isSessionValid"]),
    setActiveMenu() {
      const navLinks = document.querySelectorAll(".nav-link");

      navLinks.forEach((l) => {
        const link = l as HTMLLinkElement;

        if (l.baseURI.startsWith(link.href)) {
          l.classList.add("active");
        } else {
          l.classList.remove("active");
        }
      });
    },
    startSessionCheckTimer() {
      this.timerId = setInterval(this.timerCallback, process.env.VUE_APP_SESSION_CHECK_INTERVAL * 60000);
    },
    async timerCallback() {
      const isSessionValid = await this.isSessionValid()
        .then((result) => {
          return result;
        }).catch(error => {
          return false;
        });

      if (!isSessionValid) {
        clearInterval(this.timerId);
        this.logOut();
        this.$router.push('/sign-in');
      }
    }
  }
});
</script>
