import { Component, inject, OnInit } from '@angular/core';
import { TypeService } from '../../services/type.service';
import { GigService } from '../../services/gig.service';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  MinValidator,
  ReactiveFormsModule,
  RequiredValidator,
  Validators,
} from '@angular/forms';
import { GigRequest } from '../../interfaces/gig-request';
import { Router, RouterLinkActive } from '@angular/router';
import { Location } from '@angular/common';
import { Type } from '../../interfaces/type';
import { GigTypeSelectorComponent } from '../gig-type-selector/gig-type-selector.component';

@Component({
  selector: 'app-gig-create-screen',
  standalone: true,
  imports: [ReactiveFormsModule, GigTypeSelectorComponent],
  templateUrl: './gig-create-screen.component.html',
  styleUrl: './gig-create-screen.component.css',
})
export class GigCreateScreenComponent implements OnInit {
  ngOnInit(): void {
    this.fetchTypes();
  }
  private typeService = inject(TypeService);
  private location = inject(Location);
  private gigService = inject(GigService);
  private fb = inject(FormBuilder);
  createGigForm: FormGroup<CreateGigForm> = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    duration: [0, Validators.min(1)],
    price: [0, Validators.min(1)],
    types: [[] as number[], Validators.minLength(1)],
  });
  types: Type[] = [];

  OnSubmit() {
    const reqBody = formToRequest(this.createGigForm);
    this.gigService.createGig(reqBody).subscribe({
      next: (res) => {
        this.location.back();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  fetchTypes() {
    this.typeService.getTypes().subscribe({
      next: (res) => (this.types = res),
    });
  }
}

type CreateGigForm = {
  name: FormControl<string | null>;
  description: FormControl<string | null>;
  duration: FormControl<number | null>;
  price: FormControl<number | null>;
  types: FormControl<number[] | null>;
};

function formToRequest(form: FormGroup<CreateGigForm>): GigRequest {
  return {
    name: form.value.name ?? '',
    description: form.value.description ?? '',
    duration: form.value.duration ?? 0,
    price: form.value.price ?? 0,
    types: form.value.types ?? [],
  };
}
