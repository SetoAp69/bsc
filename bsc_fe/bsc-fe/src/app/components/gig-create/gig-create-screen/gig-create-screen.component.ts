import { Component, inject, OnInit } from '@angular/core';
import { TypeService } from '../../../services/type.service';
import { GigService } from '../../../services/gig.service';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { GigRequest } from '../../../interfaces/gig-request';
import { Location } from '@angular/common';
import { Type } from '../../../interfaces/type';
import { GigTypeSelectorComponent } from '../gig-type-selector/gig-type-selector.component';
import {
  GigRequestArrayValidator,
  GigRequestNumberValidator,
  GigRequestStringValidator,
} from '../../../validators/gig-request-validators';
import { NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { LoadingComponent } from "../../shared/loading/loading.component";
import { CreateEditGigForm, formToRequest } from '../../../interfaces/gig-create-edit-form';

@Component({
  selector: 'app-gig-create-screen',
  standalone: true,
  imports: [ReactiveFormsModule, GigTypeSelectorComponent, NgbToast, LoadingComponent],
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
  createGigForm: FormGroup<CreateEditGigForm> = this.fb.group({
    name: [
      null as string | null,
      [GigRequestStringValidator(true, 100, "Gig's name")],
    ],
    description: [
      null as string | null,
      [GigRequestStringValidator(false, 100, "Gig's name")],
    ],
    duration: [0, [GigRequestNumberValidator(1, null, 'Duration')]],
    price: [0, GigRequestNumberValidator(1, null, 'Price')],
    types: [
      [] as number[],
      [GigRequestArrayValidator(true, null, "Gig's type")],
    ],
  });
  types: Type[] = [];
  isLoading: boolean = false;
  isShowErrorToast: boolean = false;

  OnSubmit() {
    this.isLoading = true;
    const reqBody = formToRequest(this.createGigForm);
    this.gigService.createGig(reqBody).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.location.back();
      },
      error: (err) => {
        this.isLoading = false;
        this.isShowErrorToast = true;
      },
    });
  }

  fetchTypes() {
    this.typeService.getTypes().subscribe({
      next: (res) => (this.types = res),
    });
  }

  isNameError(): boolean {
    return (
      this.createGigForm.controls.name.touched &&
      this.createGigForm.getError('required')
    );
  }
}

