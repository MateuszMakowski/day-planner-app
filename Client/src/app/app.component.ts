import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Todo } from './model';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.Default,
})
export class AppComponent implements OnInit {
  public isTodoDialogOpen: boolean = true;
  public createdId = new FormControl('');
  public isLoadOfTodosComplete: boolean = false;

  public todos: Todo[] = [];

  async ngOnInit(): Promise<void> {
    await this.getTodos();
    this.isLoadOfTodosComplete = true;
  }

  constructor(private http: HttpClient) {}

  async getTodos(): Promise<void> {
    try {
      const result = await this.http
        .get<Todo[]>(environment.apiUrl + 'todos')
        .toPromise();
      this.todos = result || [];
    } catch (error) {
      throw error; // Rzucamy błąd, który można obsłużyć w odpowiednim miejscu
    }
  }

  addTodo(name: string): void {
    let todo: Todo = {
      id: '',
      name: name,
      completionFlag: false,
      date: '',
    };
    this.http
      .post<any>(environment.apiUrl + 'todos', todo)
      .subscribe((data) => {
        this.createdId = data.id;
      });
  }

  removeTodo(id: string): void {
    this.http
      .delete<any>(environment.apiUrl + 'todos/?id=' + id)
      .subscribe(() => {});
  }

  updateTodo(eventData: { id: string; completionFlag: boolean }) {
    this.http
      .put<any>(
        environment.apiUrl +
          'Todos/CompletionFlag/?id=' +
          eventData.id +
          '&completed=' +
          eventData.completionFlag,
        null
      )
      .subscribe(() => {});
  }
}
