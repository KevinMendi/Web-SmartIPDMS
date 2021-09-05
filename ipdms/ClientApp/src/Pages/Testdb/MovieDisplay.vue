<template>
    <div>
        <h1>TEST - Fetch data from database</h1>
        <pre v-if="!sampleresult"><em>Loading...</em></pre>
        <pre v-if="sampleresult">{{ sampleresult }}</pre>
    </div>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "FetchMovie",
        data() {
            return {
                sampleresult: []
            }
        },
        methods: {
            getMovieList() {
                axios.get('/api/movies')
                    .then((response) => {
                        this.sampleresult = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getMovieList();
        }
    }
</script>