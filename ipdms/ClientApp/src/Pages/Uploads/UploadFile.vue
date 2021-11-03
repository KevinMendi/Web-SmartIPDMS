<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
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
                                <form class="">
                                    <div class="position-relative form-group">
                                        <label for="exampleFile"
                                               class="">File</label><input name="file"
                                                                           id="exampleFile"
                                                                           type="file"
                                                                           @change="onFileChange"
                                                                           class="form-control-file">
                                        <small class="form-text text-muted">
                                            After selecting file to be uploaded, click button "Get Details".
                                        </small>
                                        {{imageResult}}
                                    </div>
                                    <div class="">
                                        <b-button v-b-toggle.collapseDetails block class="mr-2 mb-2" variant="info" :size="sm" :key="sm">
                                            Get Details
                                        </b-button>
                                    </div>
                                    <!-- elements to collapse -->
                                    <b-collapse id="collapseDetails" class="mt-2">

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
                                    </b-collapse>
                                </form>
                            </b-tab>
                        </b-tabs>
                    </b-card>
                </b-col>
            </b-row>
        </div>
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import FileDataService from "../../Services/FileDataService";

    export default {
        components: {
            PageTitle,

        },
        data: () => ({
            heading: 'Upload Office Action Document File',
            subheading: 'Upload latest office action document of the Project via scanning the document or upload the PDF file',
            icon: 'pe-7s-upload icon-gradient bg-happy-itmeo',

            counter: 99,
            max: 100,
            timer: null,
            striped: true,
            image: '',
            imageResult: ''

        }),
        methods: {
            onFileChange(e) {
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                this.createImage(files[0]);
            },
            createImage(file) {
                var data = {
                    image64: this.image
                }
                var reader = new FileReader();
                //var vm = this;

                reader.onload = (e) => {
                    this.image = e.target.result;
                    data.image64 = e.target.result;
                    //console.log(data);
                    FileDataService.AnalyzeImage(data)
                        .then(response => {
                            console.log(response);
                            this.imageResult = response.data;
                            //console.log(response.data);
                        })
                        .catch(e => {
                            this.imageResult = e;
                        });
                };
                reader.readAsDataURL(file);
                
            },
            analyzeImage() {
                FileDataService.AnalyzeImage(this.image)
                    .then(response => {
                        this.imageResult = response.data;
                    })
                    .catch(e => {
                        this.imageResult = e;
                    });
            }
        }


    }
</script>
