<template>
  <component :is="layout" />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import HomeLayout from "@/layouts/HomeLayout.vue";
import AuthLayout from "@/layouts/AuthLayout.vue";
import DefaultLayout from "@/layouts/DefaultLayout.vue";

export default defineComponent({
  components: {
    HomeLayout,
    AuthLayout,
    DefaultLayout,
  },
  data() {
    return {
      layout: '',
    };
  },
  watch: {
    $route(to) {
      if (to.meta.layout !== undefined) {
        this.layout = to.meta.layout
      } else {
        this.layout = "DefaultLayout"
      }
    },
  },
});
</script>

<style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    color: #2c3e50;
    height: 100vh;
  }
</style>