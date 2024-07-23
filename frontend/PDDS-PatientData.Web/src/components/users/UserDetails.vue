<template>
  <div class="user-details">
    <h3>User Details</h3>
    <div v-if="user.userId" class="pt-3">
      <div>
        <label><strong>First Name:</strong></label> {{ user.firstName }}
      </div>
      <div>
        <label><strong>Last Name:</strong></label> {{ user.lastName }}
      </div>
      <div>
        <label><strong>Email:</strong></label> {{ user.email }}
      </div>
      <div>
        <label><strong>Username:</strong></label> {{ user.username }}
      </div>
    </div>
    <div class="pt-3">
      <router-link to="/users">Back to list</router-link>&nbsp;|&nbsp;
      <router-link :to="{ name: 'user-edit', params: { id: user.userId }}">Edit</router-link>&nbsp;|&nbsp;
      <router-link to="/users" @click="this.deleteUserAction(user.userId)">Delete</router-link>
    </div>
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
    ...mapActions("User", [ "getUser", "deleteUser" ]),
    async getUserAction() {
      try {
        this.user = await this.getUser(this.$route.params.id);
      }
      catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    },
    async deleteUserAction(id: number) {
      try {
        await this.deleteUser(id);
        alert('User successfully deleted');
        this.$router.push('/users');
      }
      catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});
</script>

<style>
  .user-details {
    text-align: left;
    margin-left: 20px;
  }
</style>