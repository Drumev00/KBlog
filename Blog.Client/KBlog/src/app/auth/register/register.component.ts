import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  faUser,
  faEnvelope,
  faKey,
  faLock,
} from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../auth.service';
import { ToastrService } from 'ngx-toastr';
import ValidateForm from '../../helpers/validate-form';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  userIcon = faUser;
  emailIcon = faEnvelope;
  passwordIcon = faKey;
  repeatPasswordIcon = faLock;

  registerForm: FormGroup;
  constructor(
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.registerForm = new FormGroup(
      {
        username: new FormControl(null, [
          Validators.required,
          Validators.maxLength(50),
        ]),
        email: new FormControl(null, [Validators.required, Validators.email]),
        password: new FormControl(null, [
          Validators.required,
          Validators.minLength(6),
        ]),
        confirmPassword: new FormControl(null, [
          Validators.required,
          Validators.minLength(6),
        ]),
      },
      {
        validators: this.matchPasswords('password', 'confirmPassword').bind(
          this
        ),
      }
    );
  }

  get controls() {
    return this.registerForm.controls;
  }

  get username() {
    return this.registerForm.get('username');
  }

  get password() {
    return this.registerForm.get('password');
  }

  onSubmit() {
    if (!this.registerForm.valid) {
      console.log(this.registerForm);
      //this.toastr.error(this.registerForm.errors['error']['error']['message']);
      ValidateForm.validateAllFormFields(this.registerForm);
    } else {
      this.authService
        .registerUser(this.registerForm.value)
        .subscribe((res) => console.log(res));
    }
  }

  private matchPasswords(password: string, confirmPassword: string) {
    return (formGroup: FormGroup) => {
      const passwordControl = formGroup.controls[password];
      const confirmPasswordControl = formGroup.controls[confirmPassword];

      /*if (
        confirmPasswordControl.errors &&
        !confirmPasswordControl.errors['matchPasswords']
      ) {
        return;
      } */

      if (passwordControl.value !== confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({ matchPasswords: false });
      } else {
        confirmPasswordControl.setErrors(null);
      }
    };
  }
}
