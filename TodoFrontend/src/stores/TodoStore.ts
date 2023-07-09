import { defineStore } from 'pinia'
import TodoServices from '@/services/Todo.Service'

interface Todo {
  id: number
  title: string
  content: string
  done : boolean
}

interface TodoCreation {
  title: string
  content: string
}

export const useTodoStore = defineStore('todo', {
  actions: {
    async getMany(): Promise<Todo[]> {
      const response = await fetch(TodoServices.GetMany);
      if (!response.ok) {
        throw new Error('Failed to fetch todos');
      }
      const data = await response.json();
      return data;
    },

    async getOne(id: number): Promise<Todo> {
      const response = await fetch(TodoServices.GetOne(id));
      if (!response.ok) {
        throw new Error('Failed to fetch todo');
      }
      const data = await response.json();
      return data;
    },

    async create(model: TodoCreation) {
      const newTodo = {
        title: model.title,
        content: model.content,
      };
      await fetch(TodoServices.Create, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newTodo),
      });
    },

    async updateState(id: number) {
      await fetch(TodoServices.Update(id), {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
      })
    },

    async delete(id: number) {
      await fetch(TodoServices.Delete(id), {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
      })
    },
    async deleteAll() {
      await fetch(TodoServices.DeleteAll, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
      })
    },
  },
});
