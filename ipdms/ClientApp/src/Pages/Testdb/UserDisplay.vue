<template>
    <div>
        <h1>TEST - Fetch data from database</h1>
        <pre v-if="!sampleresult"><em>Loading...</em></pre>
        <pre v-if="sampleresult">{{ sampleresult }}</pre>
    </div>
</template>


<script>
    /*import axios from 'axios'*/
    import UserDataService from "../../Services/UserDataService";
    export default {
        name: "FetchUser",
        data() {
            return {
                sampleresult: []
            }
        },
        methods: {
            getUserList() {
        /*axios.get('/api/IpdmsUsers')*/
                UserDataService.getAllUsers()
                    .then((response) => {
                       
                        this.sampleresult = response.data;
                     /*   console.log(this.sampleresult);*/
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getUserList();
        }
    }
</script>