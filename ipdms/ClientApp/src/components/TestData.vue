<template>
    <h1 id="tableLabel">Weather forecast</h1>

    <p>This component demonstrates fetching data from the server.</p>

    <pre>{{ sampleresult }}</pre>
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
                        this.sampleresult =  response.data;
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