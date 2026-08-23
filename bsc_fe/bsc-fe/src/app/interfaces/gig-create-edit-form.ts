import { FormControl, FormGroup } from '@angular/forms';
import { GigRequest } from './gig-request';

export interface CreateEditGigForm {
  name: FormControl<string | null>;
  description: FormControl<string | null>;
  duration: FormControl<number | null>;
  price: FormControl<number | null>;
  types: FormControl<number[] | null>;
}

export function formToRequest(form: FormGroup<CreateEditGigForm>): GigRequest {
  return {
    name: form.value.name ?? '',
    description: form.value.description ?? '',
    duration: form.value.duration ?? 0,
    price: form.value.price ?? 0,
    types: form.value.types ?? [],
  };
}