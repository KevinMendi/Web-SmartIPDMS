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
                                        <b-form-checkbox value="application">Application Type/No</b-form-checkbox>
                                        <b-form-checkbox value="project">Project Name</b-form-checkbox>
                                        <b-form-checkbox value="agent_name">Agent Name</b-form-checkbox>
                                        <b-form-checkbox value="no_of_files">No. of files</b-form-checkbox>
                                    </b-form-checkbox-group>
                                </b-form-group>


                </div>
                <b-tabs pills card>
                    <b-tab title="2021" active>
                        <template>
                            <b-container fluid>
     <!--Main table element--> 
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

                                    <!--<template #cell(project_name)>

    </template>-->
                                    <template #cell(application)="row">
                                        <i class="pe-7s-folder px-2"></i>
                                        <span><router-link :to="{ name: 'project-detail', query: { projectId: row.value.projectId } }">{{ row.value.type }} {{ row.value.number }}</router-link></span>
                                        <!-- You can also use the font-awesome-icon component here -->
                                    </template>
                                    <template #cell(project)="row">
                                        {{ row.value.pname }}
                                    </template>
                                    <template #cell(numberOfFiles)="row">
                                        {{ row.value }}
                                    </template>
                                    <template #cell(agent)="row">
                                        {{ row.value.first}} {{ row.value.last }}
                                    </template>

                                    <template #cell(actions)="row">
                                        <!--ommitted script: @click="info(row.item, row.index, $event.target)"-->
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
     <!--Info modal--> 
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
    

    <!--<div id="main-content" class="file_manager">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="pe-7s-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="pe-7s-folder text-info"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Document_2017.doc</p>
                                    <small>Size: 42KB <span class="date text-muted">Nov 02, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-info"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Document_2017.doc</p>
                                    <small>Size: 89KB <span class="date text-muted">Dec 15, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-info"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Document_2017.doc</p>
                                    <small>Size: 89KB <span class="date text-muted">Dec 15, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart text-warning"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Report2016.xls</p>
                                    <small>Size: 68KB <span class="date text-muted">Dec 12, 2016</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart text-warning"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Report2016.xls</p>
                                    <small>Size: 68KB <span class="date text-muted">Dec 12, 2016</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-success"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">asdf  hhkj.pdf</p>
                                    <small>Size: 3MB <span class="date text-muted">Aug 18, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-success"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">asdf  hhkj.pdf</p>
                                    <small>Size: 3MB <span class="date text-muted">Aug 18, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-success"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">asdf  hhkj.pdf</p>
                                    <small>Size: 3MB <span class="date text-muted">Aug 18, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-success"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">asdf  hhkj.pdf</p>
                                    <small>Size: 3MB <span class="date text-muted">Aug 18, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart text-warning"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Report2016.xls</p>
                                    <small>Size: 68KB <span class="date text-muted">Dec 12, 2016</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart text-warning"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Report2017.xls</p>
                                    <small>Size: 103KB <span class="date text-muted">Jan 24, 2016</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart text-warning"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Report2016.xls</p>
                                    <small>Size: 68KB <span class="date text-muted">Dec 12, 2016</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-info"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Document_2017.doc</p>
                                    <small>Size: 42KB <span class="date text-muted">Nov 02, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-info"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Document_2017.doc</p>
                                    <small>Size: 89KB <span class="date text-muted">Dec 15, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart text-warning"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Report2017.xls</p>
                                    <small>Size: 103KB <span class="date text-muted">Jan 24, 2016</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-12">
                    <div class="card">
                        <div class="file">
                            <a href="javascript:void(0);">
                                <div class="hover">
                                    <button type="button" class="btn btn-icon btn-danger">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-file text-info"></i>
                                </div>
                                <div class="file-name">
                                    <p class="m-b-5 text-muted">Document_2017.doc</p>
                                    <small>Size: 89KB <span class="date text-muted">Dec 15, 2017</span></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>-->
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import FileDataService from "../../Services/FileDataService";

    export default {
        components: {
            PageTitle,

        },
        data() {
            return {
                heading: 'Project File Manager',
                subheading: 'List of projects categorized by year of initial application and view project\'s documents and status via hitting \'View Details\' button',
                icon: 'pe-7s-folder icon-gradient bg-happy-itmeo',

                counter: 99,
                max: 100,
                timer: null,
                striped: true,
                items: [
                    //{
                    //    isActive: false,
                    //    project_id: 0,
                    //    application: { icon: 'pe-7s-folder', type: 'Invention', number: '1/2014/000317' },
                    //    project: { pname: 'Sample Project Title 1' },
                    //    no_of_files: 2,
                    //    agent_name: { first: 'Maria', last: 'dela Cruz' }
                    //}, 
                ],
                fields: [
                    { key: 'application', label: 'Application Type/No', sortable: true, sortDirection: 'desc' },
                    { key: 'project', label: 'Project Name', sortable: true, class: 'text-center' },
                    { key: 'numberOfFiles', label: 'No. of files', sortable: true, class: 'text-center' },
                    { key: 'agent', label: 'Agent', sortable: true, sortDirection: 'desc'},
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
                },
                projectList: [],

                submitted: false,
                showAlert: false,
                alertMessage: "",
                error: null,
                user: null
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
            },
            getProjectList() {
                FileDataService.GetProjectList()
                    .then(response => {
                        console.log(response);
                        /*this.projectList = response.data;*/
                        this.items = response.data;
                        /*var result = response.data;*/




                        //this.imageResult = response.data;
                        //console.log(response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            }
        },
        beforeMount() {
            this.getProjectList();
            console.log("sulod");
            console.log(this.projectList);
        }
    }
</script>
<style>
    body {
        background-color: #f4f7f6;
        margin-top: 20px;
    }

    .file_manager .file a:hover .hover,
    .file_manager .file .file-name small {
        display: block
    }

    .file_manager .file {
        padding: 0 !important
    }

        .file_manager .file .icon {
            text-align: center
        }


    .file_manager .file {
        position: relative;
        border-radius: .55rem;
        overflow: hidden
    }

        .file_manager .file .image,
        .file_manager .file .icon {
            max-height: 180px;
            overflow: hidden;
            background-size: cover;
            background-position: top
        }

        .file_manager .file .hover {
            position: absolute;
            right: 10px;
            top: 10px;
            display: none;
            transition: all 0.2s ease-in-out
        }

        .file_manager .file a:hover .hover {
            transition: all 0.2s ease-in-out
        }

        .file_manager .file .icon {
            padding: 15px 10px;
            display: table;
            width: 100%
        }

            .file_manager .file .icon i {
                display: table-cell;
                font-size: 30px;
                vertical-align: middle;
                color: #777;
                line-height: 100px
            }

        .file_manager .file .file-name {
            padding: 10px;
            border-top: 1px solid #f7f7f7
        }

            .file_manager .file .file-name small .date {
                float: right
            }

    .folder {
        padding: 20px;
        display: block;
        color: #777
    }

    @media only screen and (max-width: 992px) {
        .file_manager .nav-tabs {
            padding-left: 0;
            padding-right: 0
        }

            .file_manager .nav-tabs .nav-item {
                display: inline-block
            }
    }

    .card {
        background: #fff;
        transition: .5s;
        border: 0;
        margin-bottom: 30px;
        border-radius: .55rem;
        position: relative;
        width: 100%;
        box-shadow: 0 1px 2px 0 rgb(0 0 0 / 10%);
    }

    a:hover {
        text-decoration: none;
    }
</style>