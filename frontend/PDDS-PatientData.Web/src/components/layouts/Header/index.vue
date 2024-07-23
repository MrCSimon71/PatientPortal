<template>
  <header id="header" class="header defaultLayout-body-pd" >
    <div class="header-toggle">
      <i id="headerToggle" class="bx bx-arrow-from-right"></i>
    </div>
    <div class="">
      <div id="menu" class="row">
        <ul class="menu">
          <li class="menu__item menu__item--dropdown" v-on:click="toggle('quickMenu')" v-bind:class="{'open' : dropDowns.quickMenu.open}">
            <div class="header-img">
              <a class="menu__link menu__link--toggle" href="#">
                <img src="https://i.imgur.com/hczKIze.jpg" class="dpicn" alt="dp">
              </a>
            </div>
            <ul class="dropdown-menu">
              <li class="dropdown-menu__item">
                <router-link to="/profile" class="dropdown-menu__link">
                  <i class="bx bx-user-circle nav_icon"></i>Profile
                </router-link>
              </li>
              <li class="dropdown-menu__item">
                <router-link to="/logout" @click="logout($event)" class="dropdown-menu__link">
                  <i class="bx bx-log-out-circle nav_icon"></i>Log Out
                </router-link>
              </li>
            </ul>
          </li>
        </ul>
      </div>
    </div>
  </header>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from 'vuex';

  export default defineComponent({
  name: "HeaderLayout",
  data() {
    return {
      appName: process.env.VUE_APP_TITLE,
      dropDowns: this.buildDropDowns()
    }
  },
  mounted() {
    window.addEventListener('click', (e: Event) => {
      const target = e.target as HTMLElement;
      const parentNode = target.parentNode as HTMLElement;

      if (!parentNode.classList.contains('menu__link--toggle')) {
        this.close();
      }
    }, false);
  },
  methods: {
    ...mapActions("Auth", [ "logOut" ]),
    async logout(event: Event) {
      event.preventDefault();

      try {
        await this.logOut();
      } catch (error) {
        console.error(error);
      }

      this.$router.push('/sign-in');
    },
    async resetPassword(event: Event) {
      event.preventDefault();

      try {
        await this.logOut();
      } catch (error) {
        console.error(error);
      }

      this.$router.push('/sign-in');
    },
    toggle: function (dropdownName: string) {
      this.dropDowns[dropdownName].open = !this.dropDowns[dropdownName].open;
    },
    close: function () {
      for (let dd in this.dropDowns) {
        this.dropDowns[dd].open = false;
      }
    },
    buildDropDowns: function () {
      let dropDowns: { [index: string]: any } = {
        quickMenu: { open: false }
      };
      return dropDowns;
    }
  }
});
</script>

<style scoped>

 /* header {
    width: 100%;
    height: 70px;
    background-color: #fff;
    box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16);
    position: sticky;
    top: 0;
    z-index: 100;
    float: right;
  }

  #topBarContent {
      margin-left: 241px;
      height:70px;
  }

  #topBarContent .left-content {
    margin-left: 20px;
    margin-top: 3px;
    width: auto;
    display: inline-flex;
    float: left;
    align-items: center;
    justify-content: center;
  }

  #topBarContent .right-content {
    float: right;
  }

  .searchbar {
      width: 250px;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .searchbar input {
    width: 500px;
    height: 42px;
    border-radius: 5px 0 0 5px;
    background-color: var(--background-color3);
    padding: 0 20px;
    font-size: 15px;
    outline: none;
    border: none;
  }

  .searchbtn {
    width: 50px;
    height: 42px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 0px 5px 5px 0px;
    background-color: var(--secondary-color);
    cursor: pointer;
  }

  .message,
  .logosec {
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .sidebar-toggle {
    margin-right: 20px;
  }

  .sidebar-toggle .nav-icon {
      cursor: pointer;
  }

     */


ul {
    list-style: none;
}

.menu {
    display: flex;
}

.menu__item {
    position: relative;
    /*padding-right: 3rem;*/
}

.menu__link {
    text-transform: uppercase;
}

.menu__link:hover {
    cursor: hourglass;
}
.menu__icon {
    margin: 0 !important;
}
.open .dropdown-menu {
    display: block;
}
.dropdown-menu {
    font-size: 0.9rem;
    position: absolute;
    min-width: 170px;
    top: 4.3rem;
    display: none;
    box-shadow: 0px 6px 12px rgba(0, 0, 0, 0.2);
    border-radius: 4px;
    left: -110px;
    height: 110px;
}
.dropdown-menu__item:first-child .dropdown-menu__link {
    border-top-left-radius: 4px;
    border-top-right-radius: 4px;
}
.dropdown-menu__item:last-child .dropdown-menu__link {
    border-bottom-left-radius: 4px;
    border-bottom-right-radius: 4px;
}

  .dropdown-menu li {
    height: 40px;
  }

  .dropdown-menu__item i {
      padding-right: 8px;
  }

  .dropdown-menu__link {
    display: block;
    padding: 1rem;
    color: #6c757d;
    background-color: #fafafa;
  }
    .dropdown-menu__link:hover {
      color: #6c757d;
      background-color: #ccc;
    }
</style>