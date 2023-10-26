import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import ValidateForm from '../../helpers/validate-form';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
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

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
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
      this.auth.login(this.loginForm.value).subscribe(
        (res) => console.log(res),
        (error) => console.log(error)
      );
      this.router.navigate(['/']);
    } else {
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }

  get email() {
    return this.loginForm.get('email');
  }

  get controls() {
    return this.loginForm.controls;
  }
}
