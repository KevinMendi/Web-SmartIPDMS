<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
        <b-alert show dismissible fade variant="danger"> Upcoming Office Action response submission deadline is on 08/09/2021! </b-alert>


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
                            <b-form-checkbox value="office_action">Office Action</b-form-checkbox>
                            <b-form-checkbox value="file">File Name</b-form-checkbox>
                            <!--<b-form-checkbox value="application_type">Agent Name</b-form-checkbox>
                        <b-form-checkbox value="isActive">Application Type</b-form-checkbox>-->
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
                            <template #cell(officeAction)="row">
                                <i class="pe-7s-file px-2"></i>
                                <span>{{ row.value.type }}</span>

                                <!-- You can also use the font-awesome-icon component here -->
                            </template>
                            <template #cell(file)="row">
                                {{ row.value.fname }}
                            </template>
                            <template #cell(fileSize)="row">
                                {{ row.value }} KB
                            </template>

                            <template #cell(actions)="row">
                                <!--ommitted script: @click="info(row.item, row.index, $event.target)"-->
                                <!--<b-button size="sm" href="file-manager/project-detail" class="mr-1" variant="info">
        View Details
    </b-button>-->
                                <b-button pill variant="success" v-b-tooltip title="View" v-on:click="previewPdfFile(row.value.folder, row.value.fname)"><i class="pe-7s-look"></i></b-button>&nbsp;
                                <b-button pill variant="warning" v-b-tooltip title="Download" v-on:click="downloadPdfFile(row.value.folder, row.value.fname)"><i class="pe-7s-download"></i></b-button>&nbsp;
                                <b-button pill variant="danger" v-b-tooltip title="Delete" @click="toggleModalDeleteDocumentById(row.value.documentId)"><i class="pe-7s-trash"></i></b-button>
                                <!--<b-button pill variant="danger" v-b-tooltip title="test"><a href="C:/kmendi/smart-ipdms\ipdms\PDF\Invention_1_2014_000318/2018_FER_PA12018050147.pdf" type="application/pdf" target="_blank">zxcvxcv</a></b-button>-->
                                    <!--<b-button pill variant="danger" v-b-tooltip title="Delete" v-on:click="deletePdfFile(row.value.documentId)"><i class="pe-7s-trash"></i></b-button>-->
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

        <!---->
        <div class="main-card mb-3 card">
            <div class="card-body">
                <div class="row">
                    <div class="col-sm-12 col-md-6 col-xl-6">
                        <div>
                            Status: <div class="mb-2 mr-2 badge badge-warning">Completion of Final Requirements</div>
                            <div class="badge badge-danger ml-2">DUE ON 6/23/2021</div>
                        </div>
                        <div>
                            Agent: <a href="javascript:void(0);" class="mb-2 mr-2 badge badge-light">Maria dela Cruz</a>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-6 col-xl-6 d-inline-flex p-2 d-flex justify-content-end">
                        <b-button class="mr-2 mb-2 border-0 btn-transition" variant="info"> + Upload </b-button>
                    </div>
                </div>

                <!-- Use dot-info : for office action that is no need for response
            dot-success : for Office Action that was successfully responded (has sumbit response date)
            dot-warning : when Office action response is not yet done, 2-3 weeks prior
            dot-danger : when 3 days or 0 days left before deadline of submission
            dot-dark : if it exceeds submission date-->
                <div>
                    <div class="divider"></div>
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div class="dot-info vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Acknowledgement</h4>
                                    <p>
                                        Received on <span class="text-success">01/01/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">N/A</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Formality Examination Report</h4>
                                    <p>
                                        Received on <span class="text-success">06/01/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">07/01/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Formality Examination Report</h4>
                                    <p>
                                        Received on <span class="text-success">08/01/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">09/01/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Formality Examination Report</h4>
                                    <p>
                                        Received on <span class="text-success">10/01/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">11/01/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Formality Examination Report</h4>
                                    <p>
                                        Received on <span class="text-success">12/23/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">12/30/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-warning vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">
                                        Notice of Publication
                                        <div class="badge badge-danger ml-2">DUE ON 6/23/2021</div>
                                    </h4>
                                    <p>
                                        Received on <span class="text-success">12/23/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">12/30/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="divider"></div>
                    <div class="row">
                        <div class="col-sm-12 col-md-6 col-xl-6 d-flex flex-column">
                            <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalFinish()" variant="success" key="success">Finish Project</b-button>
                        </div>
                        <div class="col-sm-12 col-md-6 col-xl-6 d-flex flex-column">
                            <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalConvert()" variant="warning" key="warning">Convert to Utility Model</b-button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <b-modal ref="utility-model" hide-header hide-footer title="Warning" size="sm">
            <div class="page-title-icon d-flex justify-content-center">
                <i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />
            </div>
            <div class="d-block text-center">
                This project will be converted to Utility Model. Do you want to continue?
            </div>
            <b-button class="mt-3" variant="outline-danger" block @click="hideModalConvert">Yes</b-button>
            <b-button class="mt-2" variant="outline-warning" block @click="toggleModalConvert">No</b-button>
        </b-modal>

        <b-modal ref="finish-project" hide-header hide-footer title="Warning" size="sm">
            <div class="page-title-icon d-flex justify-content-center">
                <i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />
            </div>
            <div class="d-block text-center">
                This project will be marked as finished. Do you want to continue?
            </div>
            <b-button class="mt-3" variant="outline-danger" block @click="hideModalFinish">Yes</b-button>
            <b-button class="mt-2" variant="outline-warning" block @click="toggleModalFinish">No</b-button>
        </b-modal>

        <b-modal ref="delete-document" hide-header hide-footer title="Warning" size="sm">
            <div class="page-title-icon d-flex justify-content-center">
                <i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />
            </div>
            <div class="d-block text-center">
                Are you sure you want to delete this document ?
            </div>
            <b-button class="mt-3" variant="outline-danger" block @click="deletePdfFile(deleteDocId)">Yes</b-button>
            <b-button class="mt-2" variant="outline-warning" block @click="toggleModalDelete">No</b-button>
        </b-modal>
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import FileDataService from "../../Services/FileDataService";
    import LookUpDataService from "../../Services/LookUpDataService";

    export default {
        name: 'project-detail',
        components: {
            PageTitle,

        },
        props: {
            projectId: {
                type: Number,
                required: true
            }
        },
        data() {
            return {
                heading: 'Smart Intellectual Property Document Management System',
                subheading: 'Short project description here ',
                icon: 'pe-7s-portfolio icon-gradient bg-happy-itmeo',

                counter: 99,
                max: 100,
                timer: null,
                striped: true,
                items: [
                    /*         { isActive: false, office_action: { icon: 'pe-7s-file', type: 'Acknowledgement' }, file: { fname: '2017_NOWA_PA12014000317.pdf' }, file_size: '2kb' },*/

                ],
                fields: [
                    { key: 'officeAction', label: 'Office Action/Document', sortable: true, sortDirection: 'desc' },
                    { key: 'file', label: 'File Name', sortable: true, class: 'text-center' },
                    { key: 'fileSize', label: 'File size', sortable: true, class: 'text-center' },
                    //{ key: 'agent_name', label: 'Agent', sortable: true, sortDirection: 'desc' },
                    //{
                    //    /*key: 'isActive',*/
                    //    label: 'Is Active',
                    //    formatter: (value) => {
                    //        return value ? 'Yes' : 'No'
                    //    },
                    //    sortable: true,
                    //    sortByFormatted: true,
                    //    filterByFormatted: true
                    //},
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
                pdfBase64: "",
                deleteDocId: 0
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

            hideModalConvert() {
                this.$refs['utility-model'].hide()
            },
            toggleModalConvert() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['utility-model'].toggle('#toggle-btn')
            },

            hideModalFinish() {
                this.$refs['finish-project'].hide()
            },
            toggleModalFinish() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['finish-project'].toggle('#toggle-btn')
            },
            hideModalDelete() {
                this.$refs['delete-document'].hide()
            },
            toggleModalDelete() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['delete-document'].toggle('#toggle-btn')
            },
            toggleModalDeleteDocumentById(id) {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.deleteDocId = id;
                this.$refs['delete-document'].toggle('#toggle-btn')
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
            getDocumentListByProjectId(id) {
                FileDataService.GetDocumentListByProjectId(id)
                    .then(response => {
                        console.log(response);
                        this.items = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            downloadPdfFile(folder, fname) {
                //LookUpDataService.GetDocumentById(id)
                //    .then(response => {
                //        this.pdfBase64 = response.data.pdfContent;

                //        const downloadLink = document.createElement("a");
                //        const fileName = response.data.pdfName;

                //        downloadLink.href = this.pdfBase64;
                //        downloadLink.download = fileName;
                //        downloadLink.click();
                //    })
                //    .catch(e => {
                //        this.alertMessage = e;
                //        this.error = true;
                //    });

                var link = document.createElement('a');
                link.href = `${folder}/${fname}`;
                link.download = fname;
                link.dispatchEvent(new MouseEvent('click'));
            },
            previewPdfFile(folder, fname) {
                //LookUpDataService.GetDocumentById(id)
                //    .then(response => {
                //        this.pdfBase64 = response.data.pdfContent;

                //        var byteCharacters = atob(this.pdfBase64.substring(28));
                //        var byteNumbers = new Array(byteCharacters.length);
                //        for (var i = 0; i < byteCharacters.length; i++) {
                //            byteNumbers[i] = byteCharacters.charCodeAt(i);
                //        }
                //        var byteArray = new Uint8Array(byteNumbers);
                //        var file = new Blob([byteArray], { type: 'application/pdf;base64' });
                //        var fileURL = URL.createObjectURL(file);
                //        window.open(fileURL);
                //    })
                //    .catch(e => {
                //        this.alertMessage = e;
                //        this.error = true;
                //    });
                window.open(`${folder}/${fname}`, '_blank');
            },
            deletePdfFile(id) {
                
                LookUpDataService.GetDocumentById(id)
                    .then(response => {
                        response.data.isDeleted = true;
                        console.log("deletePdfFile");
                        console.log(response.data);
                        this.flagDocumentDeleted(id, response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });

                
            },
            flagDocumentDeleted(id, data) {
                
                LookUpDataService.DeleteDocumentById(id, data)
                    .then(result => {
                        console.log(result.data);
                        this.hideModalDelete();
                        this.getDocumentListByProjectId(this.projectId);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            }
        },
        beforeMount() {
            this.getDocumentListByProjectId(this.projectId);

        }
    }
</script>
