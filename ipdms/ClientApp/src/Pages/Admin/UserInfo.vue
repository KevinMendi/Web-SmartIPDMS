<template>
    <div>
        <form @submit.prevent="updateUser">
            <div class="submit-form">
                <!--<div class="h-100" style="background-color: #38d39f;">-->
                <!--<div class="d-flex h-100 justify-content-center align-items-center">-->
                <!--<b-col md="8" class="mx-auto app-login-box">-->
                <!--<div class=" mx-auto mb-3" />-->
                <!--<div class="modal-dialog w-100">-->
                <div class="modal-content">
                    <div class="modal-body">
                        <b-alert show :variant="ipdmsVariant" dismissible fade v-model="showAlert">
                            {{alertMessage}}
                        </b-alert>
                        <h4 class="mt-2">
                            <div>User Information</div>

                        </h4>
                        <div class="divider" />
                        <select class="mb-2 form-control" name="userRoleId" id="userRoleId" v-model="user.userRoleId" v-if="action == 'r'" readonly>
                            <option :value="user.userRoleId">{{ user.userRoleId == 1 ? "Super Admin" : user.userRoleId == 2 ? "Admin" : "Agent" }}</option>
                        </select>
                        <select class="mb-2 form-control" name="userRoleId" id="userRoleId" v-model="user.userRoleId" v-if="action == 'e'">
                            <option value="-1" disabled>Please select *</option>
                            <option value="1">Super Admin</option>
                            <option value="2">Admin</option>
                            <option value="3">Agent</option>
                        </select>
                        <div class="row">
                            <div class="col-md-5">
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="firstname"
                                                  v-if="action == 'r'" readonly
                                                  name="firstname"
                                                  type="text"
                                                  v-model="user.firstname"
                                                  required
                                                  placeholder="First Name *">
                                    </b-form-input>

                                    <b-form-input id="firstname"
                                                  v-if="action == 'e'"
                                                  name="firstname"
                                                  type="text"
                                                  v-model="user.firstname"
                                                  required
                                                  placeholder="First Name *">
                                    </b-form-input>
                                </b-form-group>
                            </div>
                            <div class="col-md-5">
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="lastname"
                                                  v-if="action == 'r'" readonly
                                                  name="lastname"
                                                  type="text"
                                                  v-model="user.lastname"
                                                  required
                                                  placeholder="Last Name *">
                                    </b-form-input>

                                    <b-form-input id="lastname"
                                                  v-if="action == 'e'" 
                                                  name="lastname"
                                                  type="text"
                                                  v-model="user.lastname"
                                                  required
                                                  placeholder="Last Name *">
                                    </b-form-input>
                                </b-form-group>
                            </div>
                            <div class="col-md-2">
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="middlename"
                                                  v-if="action == 'r'" readonly
                                                  name="middlename"
                                                  type="text"
                                                  v-model="user.middlename"
                                                  placeholder="MI.">
                                    </b-form-input>

                                    <b-form-input id="middlename"
                                                  v-if="action == 'e'" 
                                                  name="middlename"
                                                  type="text"
                                                  v-model="user.middlename"
                                                  placeholder="MI.">
                                    </b-form-input>
                                </b-form-group>
                            </div>
                        </div>
                        <b-form-group id="exampleInputGroup1"
                                      label-for="exampleInput1"
                                      description="We'll never share your email with anyone else.">
                            <b-form-input id="email"
                                          v-if="action == 'r'" readonly
                                          name="email"
                                          type="email"
                                          v-model="user.email"
                                          required
                                          placeholder="Enter email *">
                            </b-form-input>
                            <b-form-input id="email"
                                          v-if="action == 'e'" 
                                          name="email"
                                          type="email"
                                          v-model="user.email"
                                          required
                                          placeholder="Enter email *">
                            </b-form-input>
                        </b-form-group>
                        <div class="row">
                            <div class="col-md-6">
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="password"
                                                  v-if="action == 'r'" readonly
                                                  name="password"
                                                  type="text"
                                                  v-model="user.password"
                                                  required
                                                  placeholder="Enter password *">
                                    </b-form-input>
                                    <b-form-input id="password"
                                                  v-if="action == 'e'" 
                                                  name="password"
                                                  type="password"
                                                  v-model="user.password"
                                                  required
                                                  placeholder="Enter password *">
                                    </b-form-input>
                                </b-form-group>
                            </div>
                            <div class="col-md-6">
                                <!--<b-button v-if="action == 'r'" readonly pill variant="success" @click="viewPassword == false ? true : false" v-b-tooltip title="View Password">View Password</b-button>-->
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="password2"
                                                  v-if="action == 'e'" 
                                                  name="password2"
                                                  type="password"
                                                  v-model="user.password2"
                                                  required
                                                  placeholder="Repeat password *">
                                    </b-form-input>
                                </b-form-group>
                            </div>
                        </div>
                        <!--<b-form-checkbox name="check" id="exampleCheck">
        Accept our <a href="javascript:void(0);">Terms and Conditions</a>.
    </b-form-checkbox>-->
                        <div class="divider" />
                        <!--<h6 class="mb-0">
        Already have an account?
        <router-link :to="{ name: 'login-boxed' }" class="text-primary">Sign in</router-link>
    </h6>-->
                    </div>
                    <div class="modal-footer d-block text-center">
                        <router-link :to="{ name: 'manage-users' }">
                            <b-button variant="warning" class="btn-wide btn-pill btn-shadow btn-hover-shine float-left"
                                      size="lg">Back</b-button>
                        </router-link>
                        <b-button type="submit" variant="primary" class="btn-wide btn-pill btn-shadow btn-hover-shine float-right" v-if="action == 'e'" readonly
                                  size="lg">
                            Update Account
                        </b-button>
                    </div>
                </div>
                <!--</div>-->
                <div class="text-center text-white opacity-8 mt-3">
                    Copyright &copy; IPDMS 2021
                </div>
                <!--</b-col>-->
                <!--</div>-->
                <!--</div>-->
            </div>
        </form>
    </div>
</template>

<script>
    import UserDataService from "../../Services/UserDataService";
    export default {
        name: "UserInfo",
        props: {
            userId: {
                type: Number,
                required: true
            },
            action: {
                //0 - read
                //1 - edit
                type: String,
                required: true
            }
            
        },
        data() {
            return {
                user: {
                    firstname: null,
                    lastname: "",
                    middlename: "",
                    userRoleId: -1,
                    email: "",
                    password: ""
                },
                submitted: false,
                showAlert: false,
                alertMessage: "",
                ipdmsVariant: 'success',
                viewPassword: false
            };
        },
        methods: {
            updateUser() {
                var data = {
                    ipdmsUserId: this.userId,
                    firstname: this.user.firstname,
                    middlename: this.user.middlename,
                    lastname: this.user.lastname,
                    userRoleId: parseInt(this.user.userRoleId),
                    email: (this.user.email).trim(),
                    password: (this.user.password).trim(),
                    password2: (this.user.password2).trim()
                };

                if ((data.password).length < 8) {
                    this.alertMessage = "Password should be greater than 6 characters!";
                    this.delayedAlert();
                }
                else if (data.password != data.password2) {
                    this.alertMessage = "Passwords do not match!";
                    this.delayedAlert();
                }
                else {
                    UserDataService.UpdateUser(this.userId, data)
                        .then(response => {
                            console.log(response.data);
                                this.alertMessage = "Successfuly Updated!";
                                this.delayedAlert();
                        })
                        .catch(e => {
                            this.alertMessage = e;
                            this.delayedAlert();
                        });
                }
            },

            delayedAlert() {
                this.showAlert = true;
                setTimeout(() => {
                    this.showAlert = false;
                }, 3000);
            },
            getUserById() {
                UserDataService.GetUserById(this.userId)
                    .then(response => {
                        console.log(response.data);
                        var data = response.data;
                        this.user.userRoleId = data.userRoleId;
                        this.user.firstname = data.firstname;
                        this.user.lastname = data.lastname;
                        this.user.middlename = data.middlename;
                        this.user.email = data.email;
                        this.user.password = data.password;
                        this.user.password2 = data.password;

                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.delayedAlert();
                    });
            }
        },
        beforeMount() {
            this.getUserById();
            console.log(this.userId);
            console.log(this.action);
        }
    }
</script>
