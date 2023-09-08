import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-import-data',
  templateUrl: './import-data.component.html',
  styleUrls: ['./import-data.component.css']
})
export class ImportDataComponent  implements OnInit {
  form!: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      start: [],
      sheet: [],
      rows: [],
      path: [''],
      connectionString: [''],
      table: [''],
      columns: this.fb.array([this.createColumn()]),
    });
    console.log(this.form);
  }

  ngOnInit() {

  }

  get columns() {
    return this.form.get('columns') as FormArray;// || this.fb.array([this.createColumn()]);
  }

  createColumn(): FormGroup {
    return this.fb.group({
      name: [''],
      type: [''],
    });
  }

  addColumn(): void {
    this.columns.push(this.createColumn());
  }

  removeColumn(index: number): void {
    this.columns.removeAt(index);
  }
}
