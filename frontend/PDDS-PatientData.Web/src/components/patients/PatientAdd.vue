<template>
  <div class="patient-add">
    <h3>Add Patient</h3>
    <form class="pb-3">
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
                id="phone"
                v-model="patientData.dateOfBirth" />
      </div>
    </form>
    <button type="submit" class="btn btn-primary" @click="addPatientAction">Save</button>
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
  methods: {
    ...mapActions("Patient", ["addPatient"]),
    async addPatientAction() {
      try {
        await this.addPatient(this.patientData);
        alert("Patient successfully added");
      } catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});
</script>

<style>
  .patient-add {
    text-align: left;
    max-width: 300px;
    margin: auto;
  }
</style>