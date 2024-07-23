<template>
  <div id="sidebar" class="sidebar-container show">
    <nav class="nav">
      <div>
        <a href="#" class="nav-logo">
          <i class="bx bx-layer nav-logo-icon"></i>
          <span class="nav-logo-name">Patient Portal</span>
        </a>
        <div class="nav-list">
          <ul id="sidebar-menu" class="nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start">
            <!-- <li class="nav-item">
              <router-link to="/dashboard" class="nav-link nav-link-top-level active">
                <i class="bx bx-tachometer nav-icon"></i>
                <span class="nav-name">Dashboard</span>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/orders" class="nav-link nav-link-top-level">
                <i class="bx bx-dollar-circle nav-icon"></i>
                <span class="nav-name">Orders</span>
              </router-link>
            </li> -->
            <!--<li class="nav-item sidebar-dropdown collapsed">
              <a href="#ordersSubmenu" data-bs-toggle="collapse" class="nav-link nav-link-top-level dropdown-link">
                <i class="bx bx-dollar-circle nav-icon"></i>
                <span class="nav-name">Orders</span>
              </a>
              <ul id="ordersSubmenu" class="collapse nav nav-submenu flex-column smooth" data-bs-parent="#sidebar-menu">
                <li class="w-100">
                  <a href="#" class="nav-link px-0"><span class="d-none d-sm-inline">Last 30 Days</span></a>
                </li>
                <li>
                  <a href="#" class="nav-link px-0"><span class="d-none d-sm-inline">Last 90 Days</span></a>
                </li>
              </ul>
            </li>-->
            <li class="nav-item">
              <router-link to="/patients" class="nav-link nav-link-top-level">
                <i class="bx bx-buildings nav-icon"></i>
                <span class="nav-name">Patients</span>
              </router-link>
            </li>
            <!-- <li class="nav-item">
              <router-link to="/products" class="nav-link nav-link-top-level">
                <i class="bx bx-cycling nav-icon"></i>
                <span class="nav-name">Products</span>
              </router-link>
            </li> -->
            <!-- <li class="nav-item">
              <router-link to="/users" class="nav-link nav-link-top-level">
                <i class="bx bx-user-circle nav-icon"></i>
                <span class="nav-name">Users</span>
              </router-link>
            </li> -->
            <!--<li class="nav-item sidebar-dropdown collapsed">
              <a href="#submenu2" data-bs-toggle="collapse" class="nav-link nav-link-top-level dropdown-link">
                <i class="bx bx-bookmark nav-icon"></i>
                <span class="nav-name">Bookmark</span>
              </a>
              <ul id="submenu2" class="collapse nav nav-submenu flex-column smooth" data-bs-parent="#sidebar-menu">
                <li>
                  <a href="#" class="nav-link px-0"><span class="d-none d-sm-inline">Item 1</span></a>
                </li>
                <li>
                  <a href="#" class="nav-link px-0"><span class="d-none d-sm-inline">Item 2</span></a>
                </li>
              </ul>
            </li>-->
            <!-- <li class="nav-item">
              <router-link to="/settings" class="nav-link nav-link-top-level">
                <i class="bx bx-cog nav-icon"></i>
                <span class="nav-name">Settings</span>
              </router-link>
            </li> -->
            <!--<li class="nav-item">
              <router-link to="#" class="nav-link nav-link-top-level">
                <i class="bx bx-bar-chart-alt-2 nav-icon"></i>
                <span class="nav-name">Stats</span>
              </router-link>
            </li>-->
          </ul>
        </div>
      </div>
      <!--<a href="#" class="nav-link">
        <i class="bx bx-log-out nav-icon"></i>
        <span class="nav-name">SignOut</span>
      </a>-->
    </nav>
  </div>
</template>

<script lang="ts">
  import { defineComponent } from "vue";

  export default defineComponent({
    name: "SidebarLayout",
    components: {

    },
    //mounted() {
    //  require('@/assets/scripts/sidebar.ts');
    //},
    methods: {
      handleRouterLinkMouseEvent(e: MouseEvent) {
        const el = e.target as HTMLElement;
        const box = el.getElementsByClassName('nav-icon')[0] as HTMLElement;
        box.style.fill = e.type == 'mouseenter' ? "#228B22" : "#ffffff";
      }
    }
  });
</script>

<style scoped>
</style>