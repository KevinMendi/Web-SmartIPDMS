<template>
    <div>
        <div class="h-100  bg-animation" style="background-color: #38d39f;">
            <div class="d-flex h-100 justify-content-center align-items-center">
                <b-col md="8" class="mx-auto app-login-box">
                    <div class=" mx-auto mb-3"/>

                    <div class="modal-dialog w-100 mx-auto">
                        <div class="modal-content">
                            <div class="modal-body">
                                <div class="h5 modal-title text-center">
                                    <h4 class="mt-2">
                                        <div>Smart Intellectual Property Document Management System</div>
                                        <small>Please sign in to your account below.</small>
                                    </h4>
                                </div>

                                <b-form-group id="exampleInputGroup1"
                                              label-for="exampleInput1"
                                              description="We'll never share your email with anyone else.">
                                    <b-form-input id="exampleInput1"
                                                  type="email"
                                                  required
                                                  placeholder="Enter email">
                                    </b-form-input>
                                </b-form-group>
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="exampleInput2"
                                                  type="password"
                                                  required
                                                  placeholder="Enter password">
                                    </b-form-input>
                                </b-form-group>
                                <b-form-checkbox name="check" id="exampleCheck">
                                    Keep me logged in
                                </b-form-checkbox>
                                <div class="divider"/>
                                <h6 class="mb-0">
                                    No account?
                                    <router-link :to="{ name: 'register-boxed' }">Sign Up</router-link>
                                </h6>
                            </div>
                            <div class="modal-footer clearfix">
                                <!--<div class="float-left">
                                    <a href="javascript:void(0);" class="btn-lg btn btn-link">Recover
                                        Password</a>
                                </div>-->
                                <div class="float-right">
                                    <!--<b-button variant="primary" size="lg">Login</b-button>-->
                                    <router-link class="btn btn-primary" size="lg" :to="{ name: 'dashboard' }">Sign in</router-link>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="text-center text-white opacity-8 mt-3">
                        Copyright &copy; IPDMS 2021
                    </div>
                </b-col>
            </div>
        </div>
    </div>
</template>
