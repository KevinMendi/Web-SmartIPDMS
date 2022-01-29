<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
        <div v-for="(item, i) in items" :key="i">
            <b-alert show dismissible fade variant="danger" v-if="item.response_date == null && (item.officeAction.id != 1 && item.officeAction.id != 9)"> {{ item.officeAction.type }} response submission deadline is on {{ item. due }}! </b-alert>
        </div>

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
                        <!--<div>
                        Status: <div class="mb-2 mr-2 badge badge-warning">Completion of Final Requirements</div>
                        <div class="badge badge-danger ml-2">DUE ON 6/23/2021</div>
                    </div>-->
                        <div>
                            <b>Agent:</b>&nbsp;&nbsp; {{ user.firstname + ' ' + user.lastname  }}<br />
                            <b>Applicant Name:</b>&nbsp;&nbsp; {{ items[0].project.applicantName }}<br />
                            <b> Application Type/No:</b>&nbsp;&nbsp; {{ items[0].project.appType + ' ' + items[0].project.appNumber }}<br />
                            <b>Title:</b>&nbsp;&nbsp; {{ items[0].project.projectTitle }}
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-6 col-xl-6 d-inline-flex p-2 d-flex justify-content-end">
                        <!--<b-button class="mr-2 mb-2 border-0 btn-transition" variant="info"> + Upload </b-button>-->
                        <b-button v-b-modal.modal-multi-1 class="mr-2 mb-2 border-0 btn-transition" variant="info">+ Upload</b-button>

                        <b-modal id="modal-multi-1" size="lg" title="First Modal" ok-only ok-variant="secondary" ok-title="Cancel" no-stacking>
                            <div class="content">
                                <b-row>
                                    <b-col md="12">
                                        <b-card class="mb-3 nav-justified" no-body>
                                            <b-tabs pills card>
                                                <b-tab title="Scan Document" active>
                                                    <form class="">
                                                        <div class="">
                                                            <b-button block class="mr-2 mb-2" variant="warning" :size="sm" :key="sm">
                                                                Open Camera to Scan the Document
                                                            </b-button>
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <label for="applicationType" class="">Application Type</label><select name="applicationType"
                                                                                                                                  id="applicationType"
                                                                                                                                  class="form-control">
                                                                <option value="1">Invention</option>
                                                                <option value="2">Utility Model</option>
                                                            </select>
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <label for="applicationNumber"
                                                                   class="">Application Number</label><input name="applicationNumber"
                                                                                                             id="applicationNumber"
                                                                                                             placeholder="Application Number"
                                                                                                             type="text"
                                                                                                             class="form-control">
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <label for="projectName"
                                                                   class="">Project Name</label><input name="projectName"
                                                                                                       id="projectName"
                                                                                                       placeholder="Project Name"
                                                                                                       type="text"
                                                                                                       class="form-control">
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <label for="officeAction"
                                                                   class="">Office Action Type</label><input name="officeAction"
                                                                                                             id="officeAction"
                                                                                                             placeholder="Office Action"
                                                                                                             type="text"
                                                                                                             class="form-control">
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <label for="officeAction" class="">Office Action:</label><select name="officeAction"
                                                                                                                             id="officeAction"
                                                                                                                             class="form-control">
                                                                <option value="1">Acknowledgement</option>
                                                                <option value="2">Formality Examination Report/Subsequent Formality Examination Report</option>
                                                                <option value="3">Notice of Publication</option>
                                                                <option value="4">Substantive Examination Report/Subsequent Substantive Report</option>
                                                                <option value="5">Completion of Final Requirements</option>
                                                                <option value="6">Notice of Allowance</option>
                                                                <option value="7">Certificate</option>
                                                                <option value="11">Notice of Issuance of Certificate</option>
                                                                <option value="12">Notice of Withdrawn Application</option>
                                                                <option value="13">Revival Order</option>
                                                                <option value="14">Notice of Forfeiture of Application</option>

                                                            </select>
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <label for="filingDate"
                                                                   class="">Document Filing Date</label><input name="filingDate"
                                                                                                               id="filingDate"
                                                                                                               placeholder="Filing Date"
                                                                                                               type="datetime"
                                                                                                               class="form-control">
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <label for="mailingDate"
                                                                   class="">Document Mailing Date</label><input name="mailingDate"
                                                                                                                id="mailingDate"
                                                                                                                placeholder="Mailing Date"
                                                                                                                type="datetime"
                                                                                                                class="form-control">
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <label for="agentName"
                                                                   class="">Agent Name</label><input name="agentName"
                                                                                                     id="agentName"
                                                                                                     placeholder="Agent Name"
                                                                                                     type="text"
                                                                                                     class="form-control">
                                                        </div>
                                                        <div class="position-relative form-group">
                                                            <div class="custom-checkbox custom-control">
                                                                <input type="checkbox"
                                                                       id="exampleCustomCheckbox"
                                                                       class="custom-control-input"><label class="custom-control-label" for="exampleCustomCheckbox">
                                                                    Please check the box if the details are all correct and click Submit.
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <button class="mt-1 btn btn-primary">Submit</button>
                                                    </form>
                                                </b-tab>
                                                <b-tab title="Upload Document" active>
                                                    <form @submit.prevent="saveProject" ref="ProjectDetailForm">
                                                        <b-overlay :show="busy"
                                                                   rounded
                                                                   opacity="0.6"
                                                                   spinner-large
                                                                   spinner-variant="primary"
                                                                   class="d-inline-block col-md-12"
                                                                   @hidden="onHidden">
                                                            <div class="card-body">
                                                                <div class="position-relative form-group">
                                                                    <label for="exampleFile"
                                                                           class="">File</label><input name="file"
                                                                                                       id="uploadFile"
                                                                                                       type="file"
                                                                                                       @change="onFileChange"
                                                                                                       class="form-control-file">
                                                                    <small class="form-text text-muted">
                                                                        After selecting file to be uploaded, click button "Get Details".
                                                                    </small>
                                                                    <!--{{imageResult}}-->
                                                                </div>
                                                                <!--<div class="">
                                                                <b-button v-b-toggle.collapseDetails block class="mr-2 mb-2" variant="info" :size="sm" :key="sm">
                                                                    Check Details
                                                                </b-button>
                                                            </div>-->
                                                                <!-- elements to collapse -->
                                                                <!--<b-collapse id="collapseDetails" class="mt-2">-->

                                                                <div class="position-relative form-group">
                                                                    <label for="applicationType" class="">Application Type</label>
                                                                    <!--<select name="applicationType" id="applicationType" class="form-control" v-model="projectIdentifier.applicationType" required>
        <option :value="projectIdentifier.applicationType"> projectIdentifier.applicationType</option>
        <option v-for="(applicationType, index) in applicationTypes" :key="index" :value="applicationType.applicationTypeId">{{ applicationType.applicationTypeName }}</option>
    </select>-->
                                                                    <input name="applicationType"
                                                                           v-if="projectIdentifier.applicationTypeName"
                                                                           id="applicationType"
                                                                           placeholder="Application Type"
                                                                           v-model="projectIdentifier.applicationTypeName"
                                                                           type="text"
                                                                           class="form-control" readonly required>

                                                                    <input name="applicationType"
                                                                           v-if="!projectIdentifier.applicationTypeName"
                                                                           id="applicationType"
                                                                           placeholder="Application Type"
                                                                           v-model="projectIdentifier.applicationTypeName"
                                                                           type="text"
                                                                           class="form-control" required>
                                                                </div>
                                                                <div class="position-relative form-group">
                                                                    <label for="applicationNumber"
                                                                           class="">Application Number</label>
                                                                    <input name="applicationNumber"
                                                                           v-if="projectIdentifier.applicationNo"
                                                                           id="applicationNumber"
                                                                           placeholder="Application Number"
                                                                           v-model="projectIdentifier.applicationNo"
                                                                           type="text"
                                                                           class="form-control" readonly required>

                                                                    <input name="applicationNumber"
                                                                           v-if="!projectIdentifier.applicationNo"
                                                                           id="applicationNumber"
                                                                           placeholder="Application Number"
                                                                           v-model="projectIdentifier.applicationNo"
                                                                           type="text"
                                                                           class="form-control" required>
                                                                </div>


                                                                <div class="position-relative form-group">
                                                                    <label for="officeAction" class="">Office Action:</label>
                                                                    <!--<select name="officeAction"
                                                                                                                                 id="officeAction"
                                                                                                                                 class="form-control">
                                                                    <option value="1">Acknowledgement</option>
                                                                    <option value="2">Formality Examination Report/Subsequent Formality Examination Report</option>
                                                                    <option value="3">Notice of Publication</option>
                                                                    <option value="4">Substantive Examination Report/Subsequent Substantive Report</option>
                                                                    <option value="5">Completion of Final Requirements</option>
                                                                    <option value="6">Notice of Allowance</option>
                                                                    <option value="7">Certificate</option>
                                                                    <option value="11">Notice of Issuance of Certificate</option>
                                                                    <option value="12">Notice of Withdrawn Application</option>
                                                                    <option value="13">Revival Order</option>
                                                                    <option value="14">Notice of Forfeiture of Application</option>

                                                                </select>-->
                                                                    <input name="officeAction"
                                                                           v-if="projectIdentifier.officeActionName"
                                                                           id="officeAction"
                                                                           placeholder="Office Action"
                                                                           v-model="projectIdentifier.officeActionName"
                                                                           type="text"
                                                                           class="form-control" readonly required>

                                                                    <input name="officeAction"
                                                                           v-if="!projectIdentifier.officeActionName"
                                                                           id="officeAction"
                                                                           placeholder="Office Action"
                                                                           v-model="projectIdentifier.officeActionName"
                                                                           type="text"
                                                                           class="form-control" required>
                                                                </div>
                                                                <div class="position-relative form-group">
                                                                    <label for="mailingDate"
                                                                           class="">Document Mailing Date</label>

                                                                    <input name="mailingDate"
                                                                           v-if="projectIdentifier.mailDate"
                                                                           id="mailingDate"
                                                                           placeholder="Mailing Date"
                                                                           v-model="projectIdentifier.mailDate"
                                                                           type="datetime"
                                                                           class="form-control" readonly required>
                                                                    <br />
                                                                    <date-picker v-if="!projectIdentifier.mailDate && !checked" v-model="projectIdentifier.mailDate" valueType="format" format="DD/MM/YYYY" required ></date-picker>&nbsp;&nbsp;&nbsp;
                                                                    <span><input type="checkbox" id="checkbox" v-model="checked"> Please check if mailing date is not applicable.</span>
                                                                </div>
                                                                <!--<div class="position-relative form-group">
                                                                <label for="agentName"
                                                                       class="">Agent Name</label><input name="agentName"
                                                                                                         id="agentName"
                                                                                                         placeholder="Agent Name"
                                                                                                         type="text"
                                                                                                         class="form-control">

                                                            </div>-->
                                                                <b-alert :class="error ? 'danger' : 'success'" show dismissible fade v-model="showAlert">
                                                                    {{alertMessage}}
                                                                </b-alert><br />
                                                                <button :disabled="disable" class="mt-1 btn btn-primary">Submit</button>
                                                                <!--</b-overlay>-->
                                                                <!--</b-collapse>-->
                                                            </div>
                                                        </b-overlay>
                                                    </form>
                                                </b-tab>
                                            </b-tabs>
                                        </b-card>
                                    </b-col>
                                </b-row>
                            </div>
                        </b-modal>
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
                        <div v-for="(item, i) in items" :key="i" :class=" item.officeAction.id == 1 ? 'dot-info vertical-timeline-element' : item.response_date == null ? 'dot-warning vertical-timeline-element' : 'dot-success vertical-timeline-element' ">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">
                                        {{ item.officeAction.type }}
                                        <div v-if="item.officeAction.id != 1 && item.response_date == null && item.mail_date != null" class="badge badge-danger ml-2">DUE {{ item.due }}</div>
                                    </h4>

                                    <p>
                                        File uploaded <span class="text-success">{{ item.created_on }}</span><br />
                                        Mail Date <span class="text-success">{{ item.officeAction.id == 9 ? "N/A" : item.mail_date }}</span><br />
                                        Submitted response on
                                        <span class="text-success">
                                            {{ item.response_date == null && (item.officeAction.id == 1 || item.officeAction.id == 9) ? "N/A" : item.response_date == null ? "Pending" : item.response_date }}
                                            <b-button v-if="item.response_date == null && (item.officeAction.id != 1 && item.officeAction.id != 9)" variant="link" v-b-tooltip title="View" @click="toggleSetResponseDate(item.actions.documentId)">(set response date)</b-button>
                                        </span>
                                        <!--<span v-if="item.response_date == null && item.officeAction.id != 1">(set response date)</span>-->

                                    </p>
                                </div>
                            </div>
                        </div>
                        <!--<div class="dot-success vertical-timeline-element">
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
                    </div>-->
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

        <b-modal ref="set-response-date" hide-header hide-footer title="Warning" size="sm">
            <!--<div class="page-title-icon d-flex justify-content-center">-->
            <!--<i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />-->
            <!--Response Date
    </div>-->
            <div class="d-block text-center">
                <h5>Response Date</h5>
                <date-picker v-model="setRDate" valueType="format" format="DD/MM/YYYY" required></date-picker>
                <div class="row mt-4">
                    <div class="col-md-6">
                        <b-button variant="outline-danger" block @click="setResponseDate(setDocId)">Set</b-button>
                    </div>
                    <div class="col-md-6">
                        <b-button variant="outline-warning" block @click="toggleModalSetResponseDate">Cancel</b-button>
                    </div>
                </div>
            </div>
            
            
        </b-modal>
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import FileDataService from "../../Services/FileDataService";
    import LookUpDataService from "../../Services/LookUpDataService";
    import UserDataService from "../../Services/UserDataService";
    import DatePicker from 'vue2-datepicker';
    import 'vue2-datepicker/index.css';

    export default {
        name: 'project-detail',
        components: {
            PageTitle,
            DatePicker,

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
                busy: false,
                disable: false,

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
                deleteDocId: 0,
                setDocId: 0,
                image: "",
                imageResult: '',
                projectIdentifier: {
                    applicationTypeId: null,
                    applicationTypeName: "",
                    applicationNo: "",
                    officeActionId: null,
                    officeActionName: "",
                    mailDate: "",
                    projectTitle: "",
                    fillingDate: null,
                    applicantName: "",
                    agentName: "",
                    fileName:"",
                    
                },
                showAlert: false,
                alertMessage: "",
                agents: [],
                applicationTypes: [],
                user: null,
                setRDate: null,
                checked: false
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
            onShown() {
                // Focus the cancel button when the overlay is showing
                this.$refs.cancel.focus()
            },
            onHidden() {
                // Focus the show button when the overlay is removed
                this.$refs.show.focus()
            },
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
            hideModalSetResponseDate() {
                this.$refs['set-response-date'].hide()
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
            toggleSetResponseDate(id) {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.setDocId = id;
                this.$refs['set-response-date'].toggle('#toggle-btn')
            },
            toggleModalSetResponseDate() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['set-response-date'].toggle('#toggle-btn')
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
            },
            //Upload Document 
            onFileChange(e) {
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;

                var fileInput = document.getElementById('uploadFile');
                this.projectIdentifier.fileName = fileInput.files[0].name;
                this.createImage(files[0]);
            },
            createImage(file) {
                var data = {
                    image64: this.image,
                    projectId: null,
                    type: 1//Upload File - FileManager
                }
                var reader = new FileReader();
                //var vm = this;

                reader.onload = (e) => {
                    this.image = e.target.result;
                    data.image64 = e.target.result;
                    data.projectId = this.projectId;
                    console.log(data);
                    this.busy = true;
                    this.disable = true;
                    FileDataService.AnalyzeImage(data)
                        .then(response => {
                            console.log(response);
                            this.busy = false;
                            this.disable = false;
                            this.imageResult = response.data;
                            this.projectIdentifier.applicationTypeId = response.data.applicationTypeId;
                            this.projectIdentifier.applicationTypeName = response.data.applicationTypeName;
                            this.projectIdentifier.applicationNo = response.data.applicationNo;
                            this.projectIdentifier.officeActionId = response.data.officeActionId;
                            this.projectIdentifier.officeActionName = response.data.officeActionName;
                            this.projectIdentifier.mailDate = response.data.mailDate;

                        })
                        .catch(e => {
                            this.alertMessage = e;
                            this.error = true;
                            this.busy = false;
                            this.disable = false;
                        });
                };
                reader.readAsDataURL(file);

            },
            getAgents() {
                UserDataService.GetAllUsers()
                    .then(response => {
                        var users = response.data;

                        var frmFilter = "3";

                        var filterAgents = Object.values(users).filter(function (entry) {
                            switch (frmFilter) {
                                case '3':
                                    return entry.userRoleId === 3;
                                default:
                                    return entry;
                            }
                        });

                        this.agents = filterAgents;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                    });
            },
            getApplicationTypes() {
                LookUpDataService.GetApplicationTypes()
                    .then(response => {
                        this.applicationTypes = response.data;
                        console.log("applicationTypes");
                        console.log(this.applicationTypes);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                    });
            },
            delayedAlert() {
                this.showAlert = true;
                setTimeout(() => {
                    this.showAlert = false;
                }, 5000);
            },
            currentDate() {
                const current = new Date();
                const date = `${current.getDate()}/${current.getMonth() + 1}/${current.getFullYear()}`;
                return date;
            },
            saveProject() {
                this.busy = true;
                this.disable = true;

                

                var data = {
                    applicationTypeId: this.projectIdentifier.applicationTypeId,
                    applicationNo: this.projectIdentifier.applicationNo,
                    projectTitle: "",
                    projectId: this.projectId,
                    officeActionId: this.projectIdentifier.officeActionId,
                    fillingDate: null,//add
                    mailingDate: this.projectIdentifier.mailDate,
                    applicantName: "",
                    agentName: "",//add
                    pdfBase64: this.image,
                    fileName: this.projectIdentifier.fileName,
                    createUserId: this.user.ipdmsUserId,
                    createUserDate: this.currentDate(),
                    lastUpdateUserId: this.user.ipdmsUserId,
                    lastUpdateDate: this.currentDate(),
                    saveType: 2
                };

                //var data2 = {
                //    pdf: this.pdfData 
                //};

                console.log(data);
                this.show = true;
                FileDataService.SaveProject(JSON.stringify(data))
                    .then(response => {
                        this.busy = false;
                        this.disable = false;
                        // this.$refs.registerProjectForm.reset();//reset form
                        this.alertMessage = response.data;
                        this.error = false;
                        this.delayedAlert();
                        //this.imageResult = response.data;
                        //console.log(response.data);
                        this.getDocumentListByProjectId(this.projectId);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                        this.busy = false;
                        this.disable = false;
                    });
            },
            setResponseDate(id) {
                console.log("setResponseDate");
                console.log(id);
                //var data = {
                //    responseDate: this.setRDate
                //};
                //console.log(data);
                //this.flagResponseDate(id, data);
                LookUpDataService.GetDocumentById(id)
                    .then(response => {
                        var dateParts = this.setRDate.split("/");
                        // month is 0-based, that's why we need dataParts[1] - 1
                        var dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0] + 1);

                        response.data.responseDate = dateObject;
                        console.log("deletePdfFile");
                        console.log(response.data);
                        this.flagResponseDate(id, response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;

                    });

                //FileDataService.SetResponseDate()
                //    .then(response => {
                //        response.data.responseDate = this.setRDate;
                //        console.log("deletePdfFile");
                //        console.log(response.data);
                //        this.flagResponseDate(id, response.data);
                //    })
                //    .catch(e => {
                //        this.alertMessage = e;
                //        this.error = true;

                //    });


            },
            flagResponseDate(id, data) {
                console.log("flagResponseDate");
                console.log(id);
                console.log(data);
                LookUpDataService.DeleteDocumentById(id, data)
                    .then(result => {
                        console.log("flagResponseDate");
                        console.log(result.data);
                        this.hideModalSetResponseDate();
                        this.getDocumentListByProjectId(this.projectId);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        console.log(e);
                        this.error = true;
                    });
            },
        },
        beforeMount() {
            this.getDocumentListByProjectId(this.projectId);
            this.getAgents();
            this.getApplicationTypes();
            this.user = JSON.parse(sessionStorage.getItem('userInfo'));

        }
    }
</script>
<style>

    .b-overlay div {
        background-color: white !important;
    }
</style>