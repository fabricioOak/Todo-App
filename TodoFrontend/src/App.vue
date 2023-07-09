<template>
  <div>
    <main class="transition-all duration-200 w-full flex flex-col items-center pt-16" id="app">
      <div>
        <img class="mx-auto" src="/todo-app.ico" alt="" />
        <h1 class="text-4xl capitalize mb-2">To do list</h1>
      </div>
      <p class="mb-8">What do you need to do?</p>
      <div class="flex flex-col justify-center items-center sm:max-w-2xl ">
        <div class="flex flex-col w-full">
          <input
            class="focus:placeholder-transparent mb-2 w-80 sm:mx-0 border-white bg-gray-800 text-center rounded text-white text-2xl py-2 focus:outline-none"
            type="text" v-model="todoTitle" placeholder="Add a title" />
          <textarea
            class="focus:placeholder-transparent w-80 sm:mx-0 border-white bg-gray-800 text-center rounded text-white text-2xl py-2 focus:outline-none"
            type="text" v-model="todoContent" placeholder="Add the content" />
        </div>

        <div class="flex">
          <button @click="addTodo"
            class="transition-all duration-200 button mt-4 text-white hover:bg-green-600 bg-green-400 mr-2">
            Add
          </button>
          <button @click="deleteAll"
            class="transition-all duration-200 button mt-4 text-white hover:bg-yellow-600 bg-yellow-400">
            Clear
          </button>

        </div>
      </div>
      <div class="w-11/12 min-w-min max-w-2xl mt-8">
        <Todo v-for="(todo, index) in todos" :key="index" :id="todo.id" :title="todo.title" :content="todo.content"
          :done="todo.done" @updateState="updateTodoState" @delete="deleteTodo" />
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import { useTodoStore } from "@/stores/TodoStore"
import { ref } from "vue";
import Todo from "./components/Todo.vue";

interface Todo {
  id: number
  title: string
  content: string
  done: boolean
}

const todoTitle = ref("");
const todoContent = ref("");
const newTodo = ref("");
const store = useTodoStore();
const todos = ref<Todo[]>([]);

store.getMany().then(
  response => todos.value = response
)

const addTodo = async () => {

  if (!todoTitle.value || !todoContent.value) return;

  await store.create({
    title: todoTitle.value,
    content: todoContent.value,
  })

  todoTitle.value = "";
  todoContent.value = "";

  await store.getMany().then(
    response => todos.value = response
  )
}

const updateTodoState = async (id: number) => {
  await store.updateState(id)

  await store.getMany().then(
    response => todos.value = response
  )
}

const deleteTodo = async (id: number) => {
  await store.delete(id)

  await store.getMany().then(
    response => todos.value = response
  )
}

const deleteAll = async () => {
  await store.deleteAll()

  await store.getMany().then(
    response => todos.value = response
  )
}

</script>

<style lang="postcss">
.button {
  @apply appearance-none border-none outline-none font-bold text-black text-2xl py-2 px-4 rounded-lg cursor-pointer;
}
</style>
