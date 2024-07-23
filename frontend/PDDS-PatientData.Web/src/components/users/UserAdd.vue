<template>
  <div class="user-add">
    <h3>Add User</h3>
    <form class="pb-3">
      <div class="form-group">
        <label for="firstName">First Name</label>
        <input type="text"
               class="form-control"
               id="firstName"
               v-model="user.firstName" />
      </div>
      <div class="form-group">
        <label for="lastName">Last Name</label>
        <input type="text"
               class="form-control"
               id="lastName"
               v-model="user.lastName" />
      </div>
      <div class="form-group">
        <label for="email">Email</label>
        <input type="text"
               class="form-control"
               id="email"
               v-model="user.email" />
      </div>
      <div class="form-group">
        <label for="username">Username</label>
        <input type="text"
               class="form-control"
               id="username"
               v-model="user.username" />
      </div>
    </form>
    <button type="submit" class="btn btn-primary" @click="addUserAction">Save</button>
    <div class="pt-3"><router-link to="/users">Back to list</router-link></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from "vuex";
import { UserModel } from "@/models/UserModel";

export default defineComponent({
  props: {},
  data() {
    return {
      user: {
        userID: null,
        firstName: '',
        lastName: '',
        email: '',
        username: '',
        phone: ''
      } as UserModel | null,
    };
  },
  methods: {
    ...mapActions("User", ["addUser"]),
    async addUserAction() {
      try {
        const userID = await this.addUser(this.user);
        if (this.user != null && userID !== '') {
          this.user.userID = userID;
          alert("User successfully added");
        }
      } catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});
</script>

<style>
  .user-add {
    text-align: left;
    max-width: 300px;
    margin: auto;
  }
</style>