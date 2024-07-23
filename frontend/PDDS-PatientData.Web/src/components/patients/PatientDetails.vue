<template>
  <div class="patient-details">
    <h3>Patient Details</h3>
    <div v-if="patientData.patientID" class="pt-3">
      <div>
        <label><strong>First Name:</strong></label> {{ patientData.firstName }}
      </div>
      <div>
        <label><strong>Last Name:</strong></label> {{ patientData.lastName }}
      </div>
      <div>
        <label><strong>Email:</strong></label> {{ patientData.email }}
      </div>
      <div>
        <label><strong>Phone:</strong></label> {{ formattedPhoneNumber(patientData.primaryPhone) }}
      </div>
    </div>
    <div>
        <label><strong>Date Of Birth:</strong></label> {{ formattedDateOfBirth(patientData.dateOfBirth) }}
    </div>
    <div class="pt-3">
      <router-link to="/patients">Back to list</router-link>&nbsp;|&nbsp;
      <router-link :to="{ name: 'patient-edit', params: { id: patientData.patientID }}">Edit</router-link>&nbsp;|&nbsp;
      <router-link to="/patients" @click="this.deletePatientAction(patientData.patientID)">Delete</router-link>
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
      patientData: {} as PatientModel | null,
    };
  },
  created() {
    this.getPatientAction();
  },
  methods: {
    ...mapActions("Patient", [ "getPatient", "deletePatient" ]),
    async getPatientAction() {
      try {
        this.patientData = await this.getPatient(this.$route.params.id);
        //console.log(this.patientData);
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
        this.$router.push('/patients');
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
  .patient-details {
    text-align: left;
    margin-left: 20px;
  }
</style>