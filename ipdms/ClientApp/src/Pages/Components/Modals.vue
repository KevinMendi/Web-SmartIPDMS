<template>
  <div>
    <page-title :heading=heading :subheading=subheading :icon=icon></page-title>

    <div class="content">
        <b-card class="main-card mb-3 text-center">
            <b-button v-b-modal.modal-multi-1>Open First Modal</b-button>

            <b-modal id="modal-multi-1" size="lg" title="First Modal" ok-only no-stacking>
                <p class="my-2">First Modal</p>
                <b-button v-b-modal.modal-multi-2>Open Second Modal</b-button>
            </b-modal>

            <b-modal id="modal-multi-2" title="Second Modal" ok-only>
                <p class="my-2">Second Modal</p>
                <b-button v-b-modal.modal-multi-3 size="sm">Open Third Modal</b-button>
            </b-modal>

            <b-modal id="modal-multi-3" size="sm" title="Third Modal" ok-only>
                <p class="my-1">Third Modal</p>
            </b-modal>

            <b-modal v-model="modalShow">dsfsdfsdfsd</b-modal>
        </b-card>
    </div>
  </div>
</template>

<script>

  import PageTitle from "../../Layout/Components/PageTitle.vue";

  export default {
    components: {
      PageTitle,

    },
    data: () => ({
      heading: 'Modals',
      subheading: 'Wide selection of modal dialogs styles and animations available.',
      icon: 'pe-7s-phone icon-gradient bg-premium-dark',



    }),


  }
</script>
