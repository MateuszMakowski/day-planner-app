import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Todo } from '../model';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css'],
  changeDetection: ChangeDetectionStrategy.Default,
})
export class TodoComponent implements OnInit, OnChanges {
  @Input() todos: any;
  @Input() createdId: FormControl = new FormControl('');
  @Output() addTodoEvent = new EventEmitter<string>();
  @Output() removeTodoEvent = new EventEmitter<string>();
  @Output() updateTodoEvent = new EventEmitter<{
    id: string;
    completionFlag: boolean;
  }>();
  public isTodoDialogOpen: boolean = true;
  public isFirstTodoBtnClicked: boolean = false;
  public formArray: any;
  public taskName: string = '';
  todoFormGroup: FormGroup = new FormGroup({
    todosFormArray: new FormArray([]),
  });

  constructor(private fb: FormBuilder) {}
  ngOnChanges(): void {
    if (this.createdId) {
      let todosFormArray = this.todoFormGroup.get(
        'todosFormArray'
      ) as FormArray;
      let index = todosFormArray.controls.length - 1;
      if (index >= 0) {
        let controlToUpdate = todosFormArray.at(index).get('id');
        controlToUpdate?.setValue(this.createdId);
      }
    }
  }

  ngOnInit(): void {
    this.loadTodos();
  }

  public loadTodos() {
    let todosFormArray = this.todoFormGroup.get('todosFormArray') as FormArray;
    todosFormArray.clear();
    this.todos.forEach((todo: any) => {
      todosFormArray.push(
        new FormGroup({
          id: new FormControl(todo.id),
          name: new FormControl(todo.name),
          completionFlag: new FormControl(todo.completionFlag),
          date: new FormControl(todo.date),
        })
      );
    });
  }

  public getTodos() {
    this.formArray = this.todoFormGroup.get('todosFormArray') as FormArray;
    return this.formArray;
  }

  addTodo(name: string) {
    if (name !== '') {
      let todosFormArray = this.todoFormGroup.get(
        'todosFormArray'
      ) as FormArray;
      todosFormArray.push(
        new FormGroup({
          id: new FormControl(''),
          name: new FormControl(name),
          completionFlag: new FormControl(false),
          date: new FormControl(''),
        })
      );
      this.taskName = '';
      this.addTodoEvent.emit(name);
    }
  }

  removeTodo(id: string) {
    let todosFormArray = this.todoFormGroup.get('todosFormArray') as FormArray;
    let index = todosFormArray.controls.findIndex(
      (x) => x.get('id')?.value === id
    );

    if (index >= 0 && id) {
      todosFormArray.removeAt(index);
      this.removeTodoEvent.emit(id);
    }
  }

  updateCompletionState(id: string) {
    let todosFormArray = this.todoFormGroup.get('todosFormArray') as FormArray;
    let index = todosFormArray.controls.findIndex(
      (x) => x.get('id')?.value === id
    );
    if (index >= 0 && id) {
      let controlToUpdate = todosFormArray.at(index).get('completionFlag');
      let completionFlag = !controlToUpdate?.value;
      controlToUpdate?.setValue(completionFlag);
      this.updateTodoEvent.emit({ id, completionFlag });
    }
  }
}
