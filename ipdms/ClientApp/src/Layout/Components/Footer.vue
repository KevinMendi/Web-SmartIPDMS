<template>
    <div class="app-footer">
        <div class="app-footer__inner text-center">
            IPDMS Copyright 2021
        </div>
    </div>
</template>

<script>

    import {library} from '@fortawesome/fontawesome-svg-core'
    import { faEllipsisV } from '@fortawesome/free-solid-svg-icons'
    // import {FontAwesomeIcon} from '@fortawesome/vue-fontawesome'

    library.add(
        faEllipsisV,
    );
    export default {
        name: "Footer",
        components: {
        }
    };
</script>
