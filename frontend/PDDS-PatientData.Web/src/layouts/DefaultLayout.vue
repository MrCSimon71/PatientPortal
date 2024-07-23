<template>
  <HeaderLayout />
  <SidebarLayout />
  <div class="main-container">
    <router-view />
  </div>
  <FooterLayout />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { HeaderLayout, SidebarLayout, FooterLayout } from "@/components/layouts";

export default defineComponent({
  components: {
    HeaderLayout,
    SidebarLayout,
    FooterLayout
  }
});
</script>

<style scoped>

.main-container {
  margin-top: 70px;
  padding: 20px 0 10px 0;
  text-align: left;
  margin-bottom: 20px;
}

</style>