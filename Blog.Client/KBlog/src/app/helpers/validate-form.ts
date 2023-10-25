import { FormControl, FormGroup } from '@angular/forms';

export default class ValidateForm {
  static validateAllFormFields(group: FormGroup) {
    Object.keys(group.controls).forEach((field) => {
      const control = group.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }
}
