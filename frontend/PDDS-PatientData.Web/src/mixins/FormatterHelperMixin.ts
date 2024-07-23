import { defineComponent } from 'vue';
import moment from 'moment'; 

export default defineComponent({
  methods: {
    formattedDistance(distance: string): string {
      const theDistance = +distance;
      return Math.round(theDistance).toString();
    },
    formattedDate(dateTime: string): string {
      if (dateTime) {
        return moment(dateTime).format("MM/DD/YYYY").toString();
      }
      return '';
    },
    formattedDateOfBirth(dateTime: string): string {
      if (dateTime) {
        return moment(dateTime).format("MM/DD/YYYY").toString();
      }
      return '';
    },
    formattedPhoneNumber(phoneNumber: string): string {
      if (phoneNumber) {
        const cleaned = ('' + phoneNumber).replace(/\\D/g, '');
        const match = cleaned.match(/^(\\d{3})(\\d{3})(\\d{4})$/);
        if (match) {
          return '(' + match[1] + ') ' + match[2] + '-' + match[3];
        }
        return phoneNumber;
      }
      return '';
    },
    formattedYesNo(value: boolean): string {
      if (value !== null || value !== undefined) {
        return value == true ? "Yes" : "No";
      }
      return '';
    },
    formattedCurrency(value: number): string {
      if (value !== null) {
        const USDollar = new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD',
        });
        return USDollar.format(value);
      }
      return '';
    },

    padNumber(value: any, length = 4) {
      return value.toString().padStart(length, 0);
    },
  }
});

