<script lang="ts">
	import * as Form from '$lib/components/ui/form';
	import { Input } from '$lib/components/ui/input';
	import * as Card from '$lib/components/ui/card/index';
	import { superForm } from 'sveltekit-superforms';
	import { zodClient } from 'sveltekit-superforms/adapters';
	import { createCategorySchema } from '$lib/models/categories.js';

	export let data;

	const form = superForm(data.form, {
		validators: zodClient(createCategorySchema)
	});

	const { form: formData, enhance } = form;
</script>

<main class="flex flex-1 flex-col gap-4 p-4 md:gap-8 md:p-8 lg:mx-auto lg:w-1/2">
	<Card.Root>
		<Card.Header>
			<h2
				class="scroll-m-20 border-b pb-2 text-3xl font-semibold tracking-tight transition-colors first:mt-0"
			>
				Create category
			</h2>
		</Card.Header>
		<Card.Content>
			<form method="POST" use:enhance>
				<Form.Field {form} name="name">
					<Form.Control let:attrs>
						<Form.Label>Name</Form.Label>
						<Input {...attrs} bind:value={$formData.name} />
					</Form.Control>
					<Form.FieldErrors />
				</Form.Field>
				<Form.Field {form} name="description">
					<Form.Control let:attrs>
						<Form.Label>Description</Form.Label>
						<Input {...attrs} bind:value={$formData.description} />
					</Form.Control>
					<Form.FieldErrors />
				</Form.Field>
				<Form.Button>Create</Form.Button>
			</form>
		</Card.Content>
	</Card.Root>
</main>
