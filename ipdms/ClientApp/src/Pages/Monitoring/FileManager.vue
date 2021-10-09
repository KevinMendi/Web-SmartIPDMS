<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
        <div class="content">
            <b-card class="mb-3" no-body>
                <div class="card-body">
                    
                            <b-input-group size="sm"
                                           cols-sm="2">
                                <b-form-input id="filter-input"
                                              v-model="filter"
                                              type="search"
                                              placeholder="Type to Search">
                                </b-form-input>
                                <b-input-group-append>
                                    <b-button :disabled="!filter" @click="filter = ''" variant="primary">Clear</b-button>
                                </b-input-group-append>
                            </b-input-group>
                            
                                <b-form-group v-model="sortDirection"
                                              label="Filter By:"
                                              description="Leave all unchecked to filter on all data"
                                              label-cols-sm="1"
                                              label-align-sm="right"
                                              label-size="sm"
                                              class="mb-0"
                                              v-slot="{ ariaDescribedby }">
                                    <b-form-checkbox-group v-model="filterOn"
                                                           :aria-describedby="ariaDescribedby"
                                                           class="mt-1">
                                        <b-form-checkbox value="project_name">Project Name</b-form-checkbox>
                                        <b-form-checkbox value="application_no">Application No.</b-form-checkbox>
                                        <b-form-checkbox value="application_type">Agent Name</b-form-checkbox>
                                        <b-form-checkbox value="isActive">Application Type</b-form-checkbox>
                                    </b-form-checkbox-group>
                                </b-form-group>
                            
                    
                </div>
                <b-tabs pills card>
                    <b-tab title="2021" active>
                        <template>
                            <b-container fluid>
                                <!-- Main table element -->
                                <b-table :items="items"
                                         :fields="fields"
                                         :current-page="currentPage"
                                         :per-page="perPage"
                                         :filter="filter"
                                         :filter-included-fields="filterOn"
                                         :sort-by.sync="sortBy"
                                         :sort-desc.sync="sortDesc"
                                         :sort-direction="sortDirection"
                                         stacked="md"
                                         show-empty
                                         small
                                         @filtered="onFiltered">
                                    <template #cell(agent_name)="row">
                                        {{ row.value.first }} {{ row.value.last }}
                                    </template>

                                    <template #cell(actions)="row">
                                        <!-- ommitted script: @click="info(row.item, row.index, $event.target)"  -->
                                        <b-button size="sm" href="file-manager/project-detail" class="mr-1" variant="info">
                                            View Details
                                        </b-button>
                                    </template>

                                    <template #row-details="row">
                                        <b-card>
                                            <ul>
                                                <li v-for="(value, key) in row.item" :key="key">{{ key }}: {{ value }}</li>
                                            </ul>
                                        </b-card>
                                    </template>
                                </b-table>

                                <!-- Info modal -->
                                <b-modal :id="infoModal.id" :title="infoModal.title" ok-only @hide="resetInfoModal">
                                    <pre>{{ infoModal.content }}</pre>
                                </b-modal>
                            </b-container>
                        </template>
                        <b-pagination align="center" :total-rows="100" v-model="currentPage" :per-page="10">
                        </b-pagination>
                    </b-tab>

                    <b-tab title="2020">
                        same as 2021 tab
                    </b-tab>
                    <b-tab title="2019">
                        same as 2021 tab
                    </b-tab>
                    <b-tab title="2018">
                        same as 2021 tab
                    </b-tab>
                </b-tabs>
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
        data() {
            return {
                heading: 'Project File Manager',
                subheading: 'List of projects categorized by year of initial application and view project\'s documents and status via hitting \'View Details\' button',
                icon: 'pe-7s-box1 icon-gradient bg-happy-itmeo',

                counter: 99,
                max: 100,
                timer: null,
                striped: true,
                items: [
                    { isActive: false, application_no: '001', application_type: 'Invention', project_name: 'Intellectual Property Document Management System', agent_name: { first: 'Maria', last: 'dela Cruz' } },
                    { isActive: false, application_no: '002', application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: { first: 'Maria', last: 'dela Cruz' }  },
                    {
                        application_no: 40, application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: { first: 'Maria', last: 'dela Cruz' } //,
                        //_rowVariant: 'success'
                    },
                    { application_no: 40, application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: { first: 'Maria', last: 'dela Cruz' }  },
                    { application_no: 40, application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: { first: 'Maria', last: 'dela Cruz' }  },
                    { application_no: 40, application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: { first: 'Maria', last: 'dela Cruz' }  },
                    { application_no: 40, application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: { first: 'Maria', last: 'dela Cruz' }  },
                //    {
                //        isActive: true,
                //        age: 87,
                //        name: { first: 'Larsen', last: 'Shaw' }//,
                //        //_cellVariants: { age: 'danger', isActive: 'warning' }
                //    },
                //    { isActive: false, age: 26, name: { first: 'Mitzi', last: 'Navarro' } },
                    { application_no: 40, application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: { first: 'Maria', last: 'dela Cruz' }  },
                    { application_no: 40, application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: { first: 'Maria', last: 'dela Cruz' }  },
                    { application_no: 40, application_type: 'Invention', project_name: 'Sample Project Title 1', agent_name: {first: 'Maria', last: 'dela Cruz'}  }
                ],
                fields: [
                    { key: 'project_name', label: 'Project Name', sortable: true, sortDirection: 'desc' },
                    { key: 'application_no', label: 'Application No', sortable: true, class: 'text-center' },
                    { key: 'application_type', label: 'Application Type', sortable: true, class: 'text-center' },
                    { key: 'agent_name', label: 'Agent', sortable: true, sortDirection: 'desc'},
                    {
                        /*key: 'isActive',*/
                        label: 'Is Active',
                        formatter: (value) => {
                            return value ? 'Yes' : 'No'
                        },
                        sortable: true,
                        sortByFormatted: true,
                        filterByFormatted: true
                    },
                    { key: 'actions', label: 'Actions', class: 'text-end' }
                ],
                totalRows: 1,
                currentPage: 1,
                perPage: 5,
                pageOptions: [5, 10, 15, { value: 100, text: "Show a lot" }],
                sortBy: '',
                sortDesc: false,
                sortDirection: 'asc',
                filter: null,
                filterOn: [],
                infoModal: {
                    id: 'info-modal',
                    title: '',
                    content: ''
                }
            }
        },
        computed: {
            sortOptions() {
                // Create an options list from our fields
                return this.fields
                    .filter(f => f.sortable)
                    .map(f => {
                        return { text: f.label, value: f.key }
                    })
            }
        },
        mounted() {
            // Set the initial number of items
            this.totalRows = this.items.length
        },
        methods: {
            info(item, index /*, button*/) {
                this.infoModal.title = `Row index: ${index}`
                this.infoModal.content = JSON.stringify(item, null, 2)
             //   this.$root.$emit('bv::show::modal', this.infoModal.id, button)
            },
            resetInfoModal() {
                this.infoModal.title = ''
                this.infoModal.content = ''
            },
            onFiltered(filteredItems) {
                // Trigger pagination to update the number of buttons/pages due to filtering
                this.totalRows = filteredItems.length
                this.currentPage = 1
            }
        }
    }
</script>
