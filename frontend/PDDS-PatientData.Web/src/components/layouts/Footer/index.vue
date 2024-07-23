<template>
  <div id="footerContent">
    <footer class="text-center">
      &copy; {{ appName }}
    </footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "FooterLayout",
  data() {
    return {
      appName: process.env.VUE_APP_TITLE,
    }
  },

});
</script>

<style scoped>

  #footerContent {
    height: 60px;
    border-top: 1px solid lightgray;
    padding-top: 20px;
  }

  footer {
    width: 100%;
    color: #5b626b;
    z-index: 1100;
  }

  .footerNavClose {
    margin-left: 77px;
  }
</style>