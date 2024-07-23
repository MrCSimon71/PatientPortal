<template>
  <div class="patient-list">
    <div class="page-header">
      <div class="page-title">Patients</div>
      <div class="header-action">
        <router-link :to="{ name: 'patient-add' }" v-slot="{ addPatient }">
          <button @click="addPatient" class="btn btn-primary">
            Add Patient
          </button>
        </router-link></div>
    </div>
    <div id="divPatientList" v-if="patientData" class="pt-3" style="font-size: 14px">
      <table style="text-align:left">
        <thead>
          <tr>
            <th>Id</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Date Of Birth</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="patient in patientData" :key="patient.patientID">
            <td width="70" class="no-wrap">{{ patient.patientID }}</td>
            <td width="120">{{ patient.firstName }}</td>
            <td width="120">{{ patient.lastName }}</td>
            <td width="200">{{ patient.email }}</td>
            <td width="200">{{ formattedPhoneNumber(patient.primaryPhone) }}</td>
            <td width="100">{{ formattedDateOfBirth(patient.dateOfBirth) }}</td>
            <td width="auto" class="ps-3">
              <router-link :to="{ name: 'patient-details', params: { id: patient.patientID }}">View</router-link>&nbsp;|&nbsp;
              <router-link :to="{ name: 'patient-edit', params: { id: patient.patientID }}">Edit</router-link>&nbsp;|&nbsp;
              <router-link to="/patients" @click="this.deletePatientAction(patient.patientID)">Delete</router-link>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from "vuex";
import { PatientModel } from "@/models/PatientModel";
import FormatterHelper from "@/mixins/FormatterHelperMixin";

export default defineComponent({
  props: {},
  mixins: [ FormatterHelper ],
  data() {
    return {
      patientData: [] as PatientModel[] | null,
    };
  },
  created() {
    this.getPatientsAction();
  },
  methods: {
    ...mapActions("Patient", ["getPatients", "deletePatient"]),
    async getPatientsAction() {
      this.patientData = null;

      try {
        this.patientData = await this.getPatients();
      }
      catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    },
    async deletePatientAction(id: number) {
      try {
        await this.deletePatient(id);
        alert('Patient successfully deleted');
        this.getPatientsAction();
      }
      catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});

</script>

<style scoped>
  .patient-list {
      margin-left: 20px;
      text-align: left;
      width: 1100px;
  }
  .page-header {
    width: 1100px;
    /*background-color: rgb(231, 231, 210);*/
  }
  .page-title {
    font-size: 24px;
    font-weight: bold;
    width: 80%;
    display: inline-block;
  }
  .header-action {
      margin-left: auto;
      /*width: 10%;*/
      display: inline-block;
  }
</style>