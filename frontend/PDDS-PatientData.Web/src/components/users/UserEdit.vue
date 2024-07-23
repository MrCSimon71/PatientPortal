<template>
  <div class="user-edit">
    <h3>Edit User</h3>
    <div v-if="user.userId" class="pt-3">
      <form>
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
      <button type="submit" class="btn btn-primary" @click="updateUserAction">
        Save
      </button>
    </div>
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
      user: {} as UserModel | null,
    };
  },
  created() {
    this.getUserAction();
  },
  methods: {
    ...mapActions("User", ["getUser", "updateUser"]),
    async getUserAction() {
      try {
        const user = await this.getUser(this.$route.params.id);
        if (user !== null) {
          this.user = {
            userID: user.userID,
            firstName: user.firstName,
            lastName: user.lastName,
            email: user.email,
            username: user.username,
          }
        }
      } catch (error) {
        console.error(error);
      }
    },
    async updateUserAction() {
      try {
        await this.updateUser(this.user);
        alert("Update successful");
      } catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    },
  //  updateUserStore(e) {

  //  },
  }
});
</script>

<style>
  .user-edit {
    text-align: left;
    max-width: 300px;
    margin: auto;
  }
</style>