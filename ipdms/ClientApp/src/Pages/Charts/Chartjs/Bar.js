import {Bar} from 'vue-chartjs'

export default {
  extends: Bar,
  mounted() {
    this.renderChart({
      labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
      datasets: [
        {
          label: '',
          backgroundColor: 'rgba(103,209,167,0.2)',
          borderColor: 'rgba(103,209,167,1)',
          borderWidth: 1,
          hoverBackgroundColor: 'rgba(103,209,167,0.4)',
          hoverBorderColor: 'rgba(103,209,167,1)',
          borderCapStyle: 'round',
          data: [65, 59, 80, 81, 56, 55, 40]
        }
      ]
    }, {responsive: true, maintainAspectRatio: false})

  }
}
