import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModal, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeService } from '../services/employee.service';

@Component({
  standalone: true,
  selector: 'app-employees',
  templateUrl: '../templates/employees.component.html',
  imports: [CommonModule, FormsModule, NgbModalModule]
})
export class EmployeesComponent implements OnInit {
  employees: any[] = [];
  sortBy: string = '';
  descending: boolean = false;
  selectedEmployee: any = {};
  modalRef: any;
  originalEmployee: any = {};

  constructor(private service: EmployeeService, private modal: NgbModal) { }

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getEmployees(this.sortBy, this.descending).subscribe(res => {
      this.employees = res.map(e => ({
        ...e,
        birthDate: this.toInputDate(e.birthDate),
        employmentDate: this.toInputDate(e.employmentDate)
      }));
    });
  }

  sort(field: string) {
    this.sortBy = field;
    this.descending = !this.descending;
    this.load();
  }

  openEdit(modal: any, employee?: any) {
    if (employee) {
      this.originalEmployee = { ...employee };
      this.selectedEmployee = {};
      this.selectedEmployee.id = employee.id;
      this.selectedEmployee.fullName = employee.fullName;
    } else {
      this.selectedEmployee = {};
      this.originalEmployee = {};
    }
    this.modalRef = this.modal.open(modal);
  }

  save() {
    const isEdit = !!this.selectedEmployee.id;

    if (isEdit) {
      const updated: any = {};

      if (this.selectedEmployee.department !== undefined) updated.department = this.selectedEmployee.department;
      if (this.selectedEmployee.fullName !== undefined) updated.fullName = this.selectedEmployee.fullName;
      if (this.selectedEmployee.birthDate) updated.birthDate = this.selectedEmployee.birthDate;
      if (this.selectedEmployee.employmentDate) updated.employmentDate = this.selectedEmployee.employmentDate;
      if (this.selectedEmployee.salary !== undefined) updated.salary = this.selectedEmployee.salary;

      this.service.updateEmployee(this.selectedEmployee.id, updated).subscribe(() => {
        this.modalRef.close();
        this.load();
      });
    } else {
      this.service.createEmployee(this.selectedEmployee).subscribe(() => {
        this.modalRef.close();
        this.load();
      });
    }
  }

  confirmDelete(confirmModal: any, employee: any) {
    this.selectedEmployee = employee;
    this.modalRef = this.modal.open(confirmModal);
  }

  delete() {
    this.service.deleteEmployee(this.selectedEmployee.id).subscribe(() => {
      this.modalRef.close();
      this.load();
    });
  }

  isCreateMode(): boolean {
    return !this.selectedEmployee?.id;
  }

  private toInputDate(dateStr: string): string {
    if (!dateStr) return '';
    const parts = dateStr.split('.');
    if (parts.length === 3) {
      return `${parts[2]}.${parts[1]}.${parts[0]}`;
    }
    return dateStr;
  }
}
