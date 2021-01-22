import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/Pagination';

import { Professor } from '../models/Professor';

@Injectable({
  providedIn: 'root'
})
export class ProfessorService {

  baseURL = `${environment.mainUrlAPI}professor`;

  constructor(
    private http: HttpClient,
  ) { }

  getAll(): Observable<Professor[]> {
    return this.http.get<Professor[]>(this.baseURL);
  }

  // getAll(page?: number, itemsPerPage?: number ): Observable<PaginatedResult<Professor[]>> {
  //   const paginatedResult: PaginatedResult<Professor[]> = new PaginatedResult<Professor[]>();

  //   let params = new HttpParams();

  //   if (page != null && itemsPerPage != null) {
  //     params = params.append('pageNumber', page.toString());
  //     params = params.append('pageSize', itemsPerPage.toString());
  //   }

  //   return this.http.get<Professor[]>(this.baseURL, { observe: 'response', params })
  //     .pipe(
  //       map(response => {
  //         paginatedResult.result = response.body;
  //         if (response.headers.get('Pagination') != null) {
  //           paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
  //         }
  //         return paginatedResult;
  //       })
  //     );
  // }


  getById(id: number): Observable<Professor> {
    return this.http.get<Professor>(`${this.baseURL}/${id}`);
  }

  getByAlunoIdAsync(id: number): Observable<Professor[]> {
    return this.http.get<Professor[]>(`${this.baseURL}/ByAluno/${id}`);
  }

  post(professor: Professor) {
    return this.http.post(this.baseURL, Professor);
  }

  put(professor: Professor) {
    return this.http.put(`${this.baseURL}/${professor.id}`, Professor);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}
