<template>
  <div class="patient-edit">
    <h3>Edit Patient</h3>
    <div v-if="patientData.patientID" class="pt-3">
      <form>
        <div class="form-group">
          <label for="firstName">First Name</label>
          <input type="text"
                 class="form-control"
                 id="firstName"
                 v-model="patientData.firstName" />
        </div>
        <div class="form-group">
          <label for="lastName">Last Name</label>
          <input type="text"
                 class="form-control"
                 id="lastName"
                 v-model="patientData.lastName" />
        </div>
        <div class="form-group">
          <label for="email">Email</label>
          <input type="text"
                 class="form-control"
                 id="email"
                 v-model="patientData.email" />
        </div>
        <div class="form-group">
          <label for="phone">Phone</label>
          <input type="text"
                 class="form-control"
                 id="phone"
                 v-model="patientData.primaryPhone" />
        </div>
        <div class="form-group">
          <label for="phone">Date of Birth</label>
          <input type="text"
                 class="form-control"
                 id="dateOfBirth"
                 v-model="patientData.dateOfBirth" />
        </div>
      </form>
      <button type="submit" class="btn btn-primary" @click="updatePatientAction">
        Save
      </button>
    </div>
    <div class="pt-3"><router-link to="/patients">Back to list</router-link></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from "vuex";
import { PatientModel } from "@/models/PatientModel";

export default defineComponent({
  props: {},
  data() {
    return {
      patientData: {} as PatientModel | null,
    };
  },
  created() {
    this.getPatientAction();
  },
  methods: {
    ...mapActions("Patient", ["getPatient", "updatePatient"]),
    async getPatientAction() {
      try {
        this.patientData = await this.getPatient(this.$route.params.id);
      } catch (error) {
        console.error(error);
      }
    },
    async updatePatientAction() {
      try {
        await this.updatePatient(this.patientData);
        alert("Update successful");
      } catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});
</script>

<style>
  .patient-edit {
    text-align: left;
    max-width: 300px;
    margin: auto;
  }
</style>