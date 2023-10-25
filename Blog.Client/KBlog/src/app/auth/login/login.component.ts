import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import ValidateForm from '../../helpers/validate-form';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  inputType: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';

  loginForm: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.maxLength(50)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  togglePass() {
    this.isText = !this.isText;
    this.isText ? (this.eyeIcon = 'fa-eye') : (this.eyeIcon = 'fa-eye-slash');
    this.isText ? (this.inputType = 'text') : (this.inputType = 'password');
  }

  onSubmit() {
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
      // send to DB
    } else {
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }

  get username() {
    return this.loginForm.get('username');
  }

  get controls() {
    return this.loginForm.controls;
  }
}
