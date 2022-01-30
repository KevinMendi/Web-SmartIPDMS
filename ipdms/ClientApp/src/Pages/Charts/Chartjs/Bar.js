import { Bar } from 'vue-chartjs'
import DashboardDataService from "../../../Services/DashboardDataService";

export default {
    extends: Bar,
    data() {
        return {
            userInfoSession: null,
            chartData: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        }
    },
    methods: {
        getProjectSummaryData() {
            DashboardDataService.GetProjectSummaryData(this.userInfoSession.ipdmsUserId, new Date().getFullYear())
                .then(response => {
                    this.chartData = response.data;
                    console.log("GetProjectSummaryData");
                    console.log(response.data);
                    this.getChart();
                })
                .catch(e => {
                    this.alertMessage = e;
                    this.error = true;
                });
        },
        getChart() {
            this.renderChart({
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                datasets: [
                    {
                        label: '',
                        backgroundColor: 'rgba(103,209,167,0.2)',
                        borderColor: 'rgba(103,209,167,1)',
                        borderWidth: 1,
                        hoverBackgroundColor: 'rgba(103,209,167,0.4)',
                        hoverBorderColor: 'rgba(103,209,167,1)',
                        borderCapStyle: 'round',
                        data: this.chartData
                    }
                ]
            }, { responsive: true, maintainAspectRatio: false })
        }
    },
    beforeMount() {
        this.userInfoSession = JSON.parse(sessionStorage.getItem('userInfo'));
        this.getProjectSummaryData();
        
    },
}
