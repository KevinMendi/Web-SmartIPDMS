<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
        <div class="content">
            
            <router-link :to="{ name: 'user-register' }">
                <b-button class="mr-3 mb-4 border-0 btn-transition float-right" variant="info">Register User</b-button>
            </router-link>
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
                            <b-form-checkbox value="name">Full Name</b-form-checkbox>
                            <b-form-checkbox value="email">Email</b-form-checkbox>
                            <b-form-checkbox value="role">Role</b-form-checkbox>
                        </b-form-checkbox-group>
                    </b-form-group>


                </div>

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
                            <!--<template #cell(application)="row">
            <i class="pe-7s-folder px-2"></i>
            <span><router-link :to="{ name: 'project-detail', query: { projectId: row.value.projectId } }">{{ row.value.type }} {{ row.value.number }}</router-link></span>-->
                            <!-- You can also use the font-awesome-icon component here -->
                            <!--</template>-->
                            <template #cell(name)="row">

                                {{ row.value.firstname + ' ' + row.value.lastname}}
                            </template>
                            <template #cell(email)="row">
                                {{ row.value }}
                            </template>
                            <template #cell(role)="row">
                                {{ row.value.desc}}
                            </template>

                            <template #cell(actions)="row">

                                <!--<b-button pill variant="success" v-b-tooltip title="View" ><i class="pe-7s-look"></i></b-button>&nbsp;-->
                                <!--<router-link tag="button" :to="{ name: 'user-info', query: { userId: row.value.userId } }"></router-link>-->
                                <router-link :to="{ name: 'user-info', query: { userId: row.value.userId, action: 'r' } }">
                                    <b-button pill variant="success" v-b-tooltip title="View"><i class="pe-7s-look"></i></b-button>
                                </router-link>&nbsp;
                                <router-link :to="{ name: 'user-info', query: { userId: row.value.userId, action: 'e' } }">
                                    <b-button pill variant="warning" v-b-tooltip title="Edit"><i class="pe-7s-pen"></i></b-button>
                                </router-link>&nbsp;
                                
                                <b-button pill variant="danger" v-b-tooltip title="Delete" @click="toggleModalDeleteUserById(row.value.userId)"><i class="pe-7s-trash"></i></b-button>
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
                    <b-modal ref="delete-user" hide-header hide-footer title="Warning" size="md">
                        <div class="page-title-icon d-flex justify-content-center">
                            <i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />
                        </div>
                        <div class="d-block text-center">
                            Are you sure you want to delete this user ?
                        </div>
                        <b-button class="mt-3" variant="outline-danger" block @click="deleteUser(deleteUserId)">Yes</b-button>
                        <b-button class="mt-2" variant="outline-warning" block @click="toggleModalDelete">No</b-button>
                    </b-modal>
                </template>
                <b-pagination align="center" :total-rows="100" v-model="currentPage" :per-page="10">
                </b-pagination>
            </b-card>
        </div>
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import UserDataService from "../../Services/UserDataService";

    export default {
        components: {
            PageTitle,

        },
        data() {
            return {
                heading: 'Manage Users',
                subheading: 'View, Add, Upate, and Delete Users',
                icon: 'pe-7s-users icon-gradient bg-happy-itmeo',

                counter: 99,
                max: 100,
                timer: null,
                striped: true,
                items: [],
                fields: [
                    { key: 'name', label: 'Full Name', sortable: true, sortDirection: 'desc' },
                    { key: 'email', label: 'Email', sortable: true, class: 'text-center' },
                    { key: 'role', label: 'Role', sortable: true, class: 'text-center' },
                    { key: 'actions', label: 'Actions', sortable: true, sortDirection: 'desc' },
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
                deleteUserId: 0
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
                this.$refs['delete-user'].toggle('#toggle-btn')
            },
            toggleModalDeleteUserById(id) {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.deleteUserId = id;
                this.$refs['delete-user'].toggle('#toggle-btn')
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
            getUserListTable() {
                UserDataService.GetUserListTable()
                    .then(response => {
                        console.log(response);
                        this.items = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            deleteUser(id) {
                UserDataService.DeleteUserById(id)
                    .then(response => {
                        console.log(response.data);
                        this.getUserListTable();
                        this.$refs['delete-user'].toggle('#toggle-btn');

                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
                
            }

        },
        beforeMount() {
            this.userInfoSession = JSON.parse(sessionStorage.getItem('userInfo'));
            this.getUserListTable();
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