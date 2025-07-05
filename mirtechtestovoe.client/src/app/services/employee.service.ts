import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  constructor(private http: HttpClient) { }

  getEmployees(sortBy?: string, descending?: boolean) {
    let params: any = {};
    if (sortBy) params.sortBy = sortBy;
    if (descending !== undefined) params.descending = descending;
    return this.http.get<any[]>('https://localhost:7032/employees', { params });
  }

  createEmployee(data: any) {
    const payload = {
      ...data,
      birthDate: this.toIsoDate(data.birthDate),
      employmentDate: this.toIsoDate(data.employmentDate)
    };
    return this.http.post('https://localhost:7032/employees', payload);
  }

  updateEmployee(id: string, data: any) {
    const payload = {
      ...data,
      birthDate: this.toIsoDate(data.birthDate),
      employmentDate: this.toIsoDate(data.employmentDate)
    };
    return this.http.put(`https://localhost:7032/employees/${id}`, payload);
  }

  deleteEmployee(id: string) {
    return this.http.delete(`https://localhost:7032/employees/${id}`);
  }

  private toIsoDate(date: any): string | null {
    if (!date) return null;
    if (typeof date === 'string' && date.match(/^\d{4}.\d{2}.\d{2}$/)) return date;
    const d = new Date(date);
    if (!isNaN(d.getTime())) {
      return d.toISOString().slice(0, 10);
    }
    return null;
  }
}
