import { AbstractControl, ValidationErrors } from "@angular/forms";

export function usernameValidator(control: AbstractControl): ValidationErrors | null {
    const valid = /^[a-zA-Z0-9_]+$/.test(control.value);
    return valid ? null : { invalidUsername: true };
}