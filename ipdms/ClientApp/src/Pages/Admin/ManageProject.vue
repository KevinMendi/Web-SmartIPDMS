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
                            <b-form-checkbox value="agent">Agent Name</b-form-checkbox>
                            <b-form-checkbox value="no_of_files">No. of files</b-form-checkbox>
                        </b-form-checkbox-group>
                    </b-form-group>


                </div>
                <!--<b-tabs pills card>
        <b-tab title="2022" active>

        </b-tab>

        <b-tab title="2021">
            same as 2021 tab
        </b-tab>
        <b-tab title="2020">
            same as 2021 tab
        </b-tab>
        <b-tab title="2019">
            same as 2021 tab
        </b-tab>
    </b-tabs>-->
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
                                <span>{{ row.value.type }} {{ row.value.number }}</span>
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
                                <router-link :to="{ name: 'project-info', query: { projectId: row.value.projectId, action: 'r' } }">
                                    <b-button pill variant="success" v-b-tooltip title="View"><i class="pe-7s-look"></i></b-button>
                                </router-link>&nbsp;
                                <router-link :to="{ name: 'project-info', query: { projectId: row.value.projectId, action: 'e' } }">
                                    <b-button pill variant="warning" v-b-tooltip title="Edit"><i class="pe-7s-pen"></i></b-button>
                                </router-link>&nbsp;

                                <b-button pill variant="danger" v-b-tooltip title="Delete" @click="toggleModalDeleteProjectById(row.value.projectId)"><i class="pe-7s-trash"></i></b-button>
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
            </b-card>
        </div>
        <b-modal ref="delete-project" hide-header hide-footer title="Warning" size="md">
            <div class="page-title-icon d-flex justify-content-center">
                <i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />
            </div>
            <div class="d-block text-center">
                Are you sure you want to delete this project ?
            </div>
            <b-button class="mt-3" variant="outline-danger" block @click="deleteProject(deleteProjectId)">Yes</b-button>
            <b-button class="mt-2" variant="outline-warning" block @click="toggleModalDelete">No</b-button>
        </b-modal>
    </div>

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
                heading: 'Manage Projects',
                subheading: 'View, Edit, and Delete Projects',
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
                    { key: 'agent', label: 'Agent', sortable: true, sortDirection: 'desc' },
                    { key: 'actions', label: 'Actions', sortable: true, sortDirection: 'desc' },
                    //{
                    //    key: 'isActive',
                    //    label: 'Is Active',
                    //    formatter: (value) => {
                    //        return value ? 'Yes' : 'No'
                    //    },
                    //    sortable: true,
                    //    sortByFormatted: true,
                    //    filterByFormatted: true
                    //},
                  /*  { key: 'actions', label: 'Actions', class: 'text-end' }*/
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
                user: null,
                userInfoSession: null,
                deleteProjectId: 0,
                project: {
                    appTypeId: -1,
                    appNumber: "",
                    applicantName: "",
                    projectTitle: "",
                    projectStatusId: 0,
                    agentId: 0,
                    createUserId: 0,
                    lastUpdateUserId: 0
                },
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
            toggleModalDelete() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['delete-project'].toggle('#toggle-btn')
            },
            toggleModalDeleteProjectById(id) {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.deleteProjectId = id;
                this.$refs['delete-project'].toggle('#toggle-btn')
            },
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
                FileDataService.GetProjectList(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId, new Date().getFullYear())
                    .then(response => {
                        console.log(response);
                        this.items = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            updateProject(id) {
                
                var data = {
                    projectId: id,
                    applicationTypeId: this.project.appTypeId,
                    projectStatusId: this.project.projectStatusId,
                    ipdmsUserId: this.project.agentId,
                    applicationNo: this.project.appNumber.trim(),
                    projectTitle: this.project.projectTitle.trim(),
                    applicantName: this.project.applicantName.trim(),
                    createUserId: this.project.createUserId,
                    lastUpdateUserId: this.project.lastUpdateUserId,
                    isDeleted: true
                };

                FileDataService.UpdateProject(id, data)
                    .then(response => {
                        console.log(response.data);
                        this.getProjectList();
                        this.$refs['delete-project'].toggle('#toggle-btn');
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.delayedAlert();
                    });
            },
            deleteProject(id) {
                FileDataService.GetProjectById(id)
                    .then(response => {
                        var data = response.data;
                        console.log("data");
                        console.log(data);
                        this.project.appTypeId = data.project.appTypeId;
                        this.project.appNumber = data.project.appNumber;
                        this.project.applicantName = data.project.applicantName;
                        this.project.projectTitle = data.project.projectTitle;
                        this.project.projectStatusId = data.project.projectStatusId;
                        this.project.agentId = data.project.agentId;
                        this.project.createUserId = data.project.createUserId;
                        this.project.lastUpdateUserId = data.project.lastUpdateUserId;

                        console.log("project");
                        console.log(this.project);

                        this.updateProject(id);

                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.delayedAlert();
                    });
            },
            //deleteProject(id) {
            //    FileDataService.UpdateProject(id)
            //        .then(response => {
            //            console.log(response.data);
            //            this.getProjectList();
            //            this.$refs['delete-project'].toggle('#toggle-btn');

            //        })
            //        .catch(e => {
            //            this.alertMessage = e;
            //            this.error = true;
            //        });

            //}
        },
        beforeMount() {
            this.userInfoSession = JSON.parse(sessionStorage.getItem('userInfo'));
            this.getProjectList();


            console.log("userInfo");
            console.log(this.userInfoSession);
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