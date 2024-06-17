<script lang="ts">
	import * as Form from '$lib/components/ui/form';
	import * as Select from '$lib/components/ui/select';
	import { Input } from '$lib/components/ui/input';
	import * as Card from '$lib/components/ui/card/index';
	import SuperDebug, { superForm } from 'sveltekit-superforms';
	import { zodClient } from 'sveltekit-superforms/adapters';
	import {
		PaymentMethod,
		TransactionType,
		createTransactionSchema
	} from '$lib/transactions/models.js';
	import { browser } from '$app/environment';
	import { page } from '$app/stores';
	import type { Selected } from 'bits-ui';
	import type { Profile } from '$lib/profiles/models.js';
	import { Switch } from '$lib/components/ui/switch/index.js';

	export let data;

	const { profiles, categories } = data;

	const form = superForm(data.form, {
		validators: zodClient(createTransactionSchema)
	});

	const { form: formData, enhance } = form;

	const [selectedProfile, onProfileChange] = createSelection<string>(
		(s) => ($formData.profileId = s.value),
		undefined
	);

	const [selectedCategory, onCategoryChange] = createSelection<string>(
		(s) => ($formData.categoryId = s.value),
		undefined
	);

	const [selectedTransactionType, onTransactionTypeChange] = createSelection<TransactionType>(
		(s) => ($formData.type = s.value),
		{
			value: TransactionType.Income,
			label: TransactionType.Income
		}
	);

	const [selectedPaymentMethod, onPaymentMethodChange] = createSelection<PaymentMethod>(
		(s) => ($formData.paymentMethod = s.value)
	);

	function createSelection<T>(
		formUpdate: (s: Selected<T>) => void,
		initialValue?: Selected<T> | undefined
	) {
		let selected = initialValue;
		function onSelection(s: Selected<T>) {
			selected = {
				...s
			};
			formUpdate(s);
		}
		return [selected, onSelection] as const;
	}
</script>

<main class="flex flex-1 flex-col gap-4 p-4 md:gap-8 md:p-8 lg:mx-auto lg:w-1/2">
	<Card.Root>
		<Card.Header>
			<h2
				class="scroll-m-20 border-b pb-2 text-3xl font-semibold tracking-tight transition-colors first:mt-0"
			>
				Create profile
			</h2>
		</Card.Header>
		<Card.Content>
			<form method="POST" use:enhance class="lg:px-6">
				<Form.Field {form} name="profileId">
					<Form.Control let:attrs>
						<Form.Label>Profile</Form.Label>
						<Select.Root
							selected={selectedProfile}
							onSelectedChange={(e) => e && onProfileChange(e)}
						>
							<Select.Input name={attrs.name} />
							<Select.Trigger>
								<Select.Value placeholder="Select profile" />
							</Select.Trigger>
							<Select.Content>
								{#each profiles as profile}
									<Select.Item value={profile.id} label={profile.name} />
								{/each}
							</Select.Content>
						</Select.Root>
					</Form.Control>
					<Form.FieldErrors />
				</Form.Field>

				<Form.Field {form} name="amount">
					<Form.Control let:attrs>
						<Form.Label>Amount</Form.Label>
						<Input type="number" {...attrs} bind:value={$formData.amount} />
					</Form.Control>
					<Form.FieldErrors />
				</Form.Field>

				<Form.Field {form} name="categoryId">
					<Form.Control let:attrs>
						<Form.Label>Category</Form.Label>
						<Select.Root
							selected={selectedCategory}
							onSelectedChange={(e) => e && onCategoryChange(e)}
						>
							<Select.Input name={attrs.name} />
							<Select.Trigger>
								<Select.Value placeholder="Select category" />
							</Select.Trigger>
							<Select.Content>
								{#each categories as category}
									<Select.Item value={category.id} label={category.name} />
								{/each}
							</Select.Content>
						</Select.Root>
					</Form.Control>
					<Form.FieldErrors />
				</Form.Field>

				<Form.Field {form} name="type">
					<Form.Control let:attrs>
						<Form.Label>Transaction type</Form.Label>
						<Select.Root
							selected={selectedTransactionType}
							onSelectedChange={(e) => e && onTransactionTypeChange(e)}
						>
							<Select.Input name={attrs.name} />
							<Select.Trigger>
								<Select.Value placeholder="Select transaction type" />
							</Select.Trigger>
							<Select.Content>
								{#each Object.values(TransactionType) as transactionType}
									<Select.Item value={transactionType} label={transactionType} />
								{/each}
							</Select.Content>
						</Select.Root>
					</Form.Control>
					<Form.FieldErrors />
				</Form.Field>

				<Form.Field {form} name="isRecurring">
					<Form.Control let:attrs>
						<Form.Label>Is recurring</Form.Label>
						<Switch includeInput {...attrs} bind:checked={$formData.isRecurring} />
					</Form.Control>
					<Form.FieldErrors />
				</Form.Field>

				<Form.Field {form} name="paymentMethod">
					<Form.Control let:attrs>
						<Form.Label>Payment method</Form.Label>
						<Select.Root
							selected={selectedPaymentMethod}
							onSelectedChange={(e) => e && onPaymentMethodChange(e)}
						>
							<Select.Input name={attrs.name} />
							<Select.Trigger>
								<Select.Value placeholder="Select payment method" />
							</Select.Trigger>
							<Select.Content>
								{#each Object.values(PaymentMethod) as paymentMethod}
									<Select.Item value={paymentMethod} label={paymentMethod} />
								{/each}
							</Select.Content>
						</Select.Root>
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

				<Form.Field {form} name="merchant">
					<Form.Control let:attrs>
						<Form.Label>Merchant</Form.Label>
						<Input {...attrs} bind:value={$formData.merchant} />
					</Form.Control>
					<Form.FieldErrors />
				</Form.Field>

				<Form.Button>Submit</Form.Button>

				{#if browser}
					<SuperDebug data={$formData} />
				{/if}
			</form>
		</Card.Content>
	</Card.Root>
</main>
